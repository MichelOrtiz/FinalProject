using System;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SaveFilesManager : MonoBehaviour
{
    //string filePath;
    //string jsonString;
    public SaveFile currentSaveSlot {
        get {
            if(partida==null){
                Debug.Log("Usando partida tester"); //asignar savefile de tester
                startSession = DateTime.Now;
                string filePath = Application.dataPath + "/Partida3"; //ubicacion de la partida
                partida = LoadSaveFile(filePath);
                if(partida == null){
                    partida = new SaveFile("PTest",3);
                    //partida.controlbinds = KeybindManager.instance.controlbinds;
                    partida.controlBindsKeys = KeybindManager.defaultKeys.ToList<string>();
                    partida.controlBindsValues = KeybindManager.defaultValues.ToList<KeyCode>();
                    WriteSaveFile(partida,filePath);
                }
                SceneController.instance.prevScene = -1; 
            }
            
            return partida;
        }
        set{
            partida = value;
        }
    }
    private SaveFile partida;
    private DateTime startSession;
    public static SaveFilesManager instance;
    private void Awake() {
        if(instance == null) instance = this;
        else if(instance!=this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
    public SaveFile LoadSaveFile(string path){
        string jsonString;
        if(File.Exists(path)){
            using(StreamReader reader = new StreamReader(path)){
                jsonString = reader.ReadToEnd();
            }
            if(jsonString == "" || jsonString == null){
                //Debug.Log("Null reference");
                return null;
            } 
        }else{
            FileStream stream = File.Create(path);
            stream.Close();
            WriteSaveFile(null,path);
            return null;
        }
        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(jsonString);
        //Debug.Log(saveFile.ToString());
        return saveFile;
    }
    public void WriteSaveFile(SaveFile file,string filePath){
        string jsonString;
        if(file != null){
            jsonString = JsonUtility.ToJson(file);
        }else{
            jsonString = "";
        }
        if(!File.Exists(filePath)){
            FileStream stream = File.Create(filePath);
            stream.Close();
            }
        using(StreamWriter writer = new StreamWriter(filePath,false)){
            writer.Write(jsonString);
        }
    }
    public void DeleteSaveFile(string path){
        if(File.Exists(path)){
            File.Delete(path);
        }
        FileStream stream = File.Create(path);
        stream.Close();
    }
    public void SaveProgress(){
        if(currentSaveSlot==null)return;
        //Tiempo de juego
        TimeSpan timePlayed = DateTime.Now - startSession;
        Debug.Log("Tiempo de juego: " + timePlayed.Hours + ":" + timePlayed.Minutes + ":" + timePlayed.Seconds);
        //Debug.Log(DateTime.Now.ToString() + " - " + startSession.ToString());
        currentSaveSlot.timeSecondsPlayed += timePlayed.Seconds;
        while(currentSaveSlot.timeSecondsPlayed >= 60){
            currentSaveSlot.timeSecondsPlayed -= 60;
            currentSaveSlot.timeMinutesPlayed += 1;
        }
        currentSaveSlot.timeMinutesPlayed += timePlayed.Minutes;
        while(currentSaveSlot.timeMinutesPlayed >= 60){
            currentSaveSlot.timeMinutesPlayed -= 60;
            currentSaveSlot.timeHoursPlayed += 1;
        }
        currentSaveSlot.timeHoursPlayed += timePlayed.Hours;
        currentSaveSlot.staminaLimit = PlayerManager.instance.currentStaminaLimit;
        //Abilidades
        int i = 0;
        currentSaveSlot.abilitiesBinds = new List<KeyCode>();
        currentSaveSlot.unlockedAbilities = new bool[AbilityManager.instance.abilities.Count];
        foreach(Ability a in AbilityManager.instance.abilities){
            currentSaveSlot.unlockedAbilities[i] = a.isUnlocked;
            currentSaveSlot.abilitiesBinds.Add(a.hotkey);
            i++;
        }
        //Inventario
        currentSaveSlot.money = Inventory.instance.GetMoney();
        currentSaveSlot.capacidad = Inventory.instance.capacidad;
        //Configuraciones
        SettingsMenu sm = FindObjectOfType<Pause>().settingsMenu;
        currentSaveSlot.masterVol = sm.masterVol;
        currentSaveSlot.hazardVol = sm.hazardVol;
        currentSaveSlot.musicVol = sm.musicVol;
        currentSaveSlot.qualityIndex = sm.qualityIndex;
        currentSaveSlot.isFullScreen = sm.isFullScreen;

        string jsonString = JsonUtility.ToJson(currentSaveSlot);
        string filePath = Application.dataPath + "/Partida" + currentSaveSlot.slotFile;
        using(StreamWriter writer = new StreamWriter(filePath,false)){
            writer.Write(jsonString);
        }
    }
    public void SetStartSession(DateTime dateStart){
        startSession = dateStart;
    }
}

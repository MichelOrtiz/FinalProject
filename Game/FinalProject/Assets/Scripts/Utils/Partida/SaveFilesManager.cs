using System;
using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

public class SaveFilesManager : MonoBehaviour
{
    //string filePath;
    //string jsonString;
    public SaveFile currentSaveSlot {get;set;}
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
            File.Create(path);
            WriteSaveFile(null,path);
            return null;
        }
        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(jsonString);
        Debug.Log(saveFile.ToString());
        return saveFile;
    }
    public void WriteSaveFile(SaveFile file,string filePath){
        string jsonString;
        if(file != null){
            jsonString = JsonUtility.ToJson(file);
        }else{
            jsonString = "";
        }
        if(!File.Exists(filePath))File.Create(filePath);
        using(StreamWriter writer = new StreamWriter(filePath,false)){
            writer.Write(jsonString);
        }
    }
    public void DeleteSaveFile(string path){
        if(File.Exists(path)){
            File.Delete(path);
        }
        File.Create(path);
    }
    public void SaveProgress(){
        if(currentSaveSlot==null)return;
        TimeSpan timePlayed = DateTime.Now - startSession;
        Debug.Log(DateTime.Now.ToString() + " - " + startSession.ToString());
        currentSaveSlot.timeSecondsPlayed += timePlayed.Seconds;
        if(currentSaveSlot.timeSecondsPlayed >= 60){
            currentSaveSlot.timeSecondsPlayed -= 60;
            currentSaveSlot.timeMinutesPlayed += 1;
        }
        currentSaveSlot.timeMinutesPlayed += timePlayed.Minutes;
        if(currentSaveSlot.timeMinutesPlayed >= 60){
            currentSaveSlot.timeMinutesPlayed -= 60;
            currentSaveSlot.timeHoursPlayed += 1;
        }
        currentSaveSlot.timeHoursPlayed += timePlayed.Hours;
        
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

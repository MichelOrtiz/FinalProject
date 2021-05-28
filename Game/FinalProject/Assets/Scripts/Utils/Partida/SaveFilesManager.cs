using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

public class SaveFilesManager : MonoBehaviour
{
    string filePath;
    string jsonString;
    public int currentSaveSlot;
    public static SaveFilesManager instance;
    private void Awake() {
        if(instance == null) instance = this;
        else if(instance!=this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public SaveFile LoadSaveFile(string path){
        jsonString = File.ReadAllText(path);
        SaveFile saveFile = new SaveFile();
        saveFile = JsonUtility.FromJson<SaveFile>(jsonString);
        return saveFile;
    }
    public void WriteSaveFile(SaveFile file){
        filePath = Application.dataPath + "/Partida" + file.slotFile;
        jsonString = JsonUtility.ToJson(file);
        File.WriteAllText(filePath,jsonString);
    }
}

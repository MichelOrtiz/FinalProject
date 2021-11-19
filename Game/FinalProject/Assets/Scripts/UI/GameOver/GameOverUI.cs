using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //turn off all UI elements?
        
    }
    void Update(){
        PlayerManager.instance.SetEnabledPlayer(false);
    }
    public void SendToMainMenu(){
        SceneController.instance.LoadScene(0);
        Destroy(gameObject);
    }
    public void SendToCheckPoint(){
        string path = Application.dataPath + "/Partida" + SaveFilesManager.instance.currentSaveSlot.slotFile;
        SaveFile newPartida = SaveFilesManager.instance.LoadSaveFile(path);
        SaveFilesManager.instance.currentSaveSlot = newPartida;
        SceneController.instance.Load(newPartida);
        Destroy(gameObject);
    }
}

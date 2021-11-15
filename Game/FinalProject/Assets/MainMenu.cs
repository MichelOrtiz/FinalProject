using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private void Start() {
        for(int i = 0; i < 4; i++){
            string filePath = Application.dataPath + "/Partida" + i;
            if(!File.Exists(filePath)){
                FileStream stream = File.Create(filePath);
                stream.Close();
            }
        }
        Destroy(PlayerManager.instance?.gameObject);
        Destroy(Inventory.instance?.gameObject);
        Destroy(CameraFollow.instance?.gameObject);
    }
    public void QuitGame(){
        Application.Quit();
    }
}

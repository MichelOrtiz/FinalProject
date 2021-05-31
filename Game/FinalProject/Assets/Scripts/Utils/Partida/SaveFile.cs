using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFile
{
    public int slotFile;
    public string namePlayer;
    public string timePlayed;
    public int sceneToLoad;
    public Vector2 positionSpawn;
    public Item[] inventoy;
    public Item[] chestItems;
    public GameObject prefab;
    public SaveFile(){
        this.slotFile = 0;
        this.namePlayer = "NoName";
        this.timePlayed = "00:00:00";
        this.sceneToLoad = 1;
        this.positionSpawn = new Vector2(0f,0f);
        this.inventoy = null;
        this.chestItems = null;
    }
    public SaveFile(string namePlayer, int slotFile){
        this.namePlayer = namePlayer;
        this.slotFile = slotFile;
        this.timePlayed = "00:00:00";
        this.sceneToLoad = 1;
        this.positionSpawn = new Vector2(0f,0f);
        this.inventoy = null;
        this.chestItems = null;
    }
}

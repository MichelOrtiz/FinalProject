using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFile
{
    public int slotFile;
    public string namePlayer;
    public int timeMinutesPlayed;
    public int timeHoursPlayed;
    public int timeDaysPlayed;
    public int sceneToLoad;
    public Vector2 positionSpawn;
    public Item[] inventory;
    public Item[] chestItems;
    public GameObject prefab;
    public SaveFile(){
        this.slotFile = 0;
        this.namePlayer = "NoName";
        this.timeMinutesPlayed = 0;
        this.timeHoursPlayed = 0;
        this.timeDaysPlayed = 0;
        this.sceneToLoad = 1;
        this.positionSpawn = new Vector2(0f,0f);
        this.inventory = null;
        this.chestItems = null;
    }
    public SaveFile(string namePlayer, int slotFile){
        this.namePlayer = namePlayer;
        this.slotFile = slotFile;
        this.timeMinutesPlayed = 0;
        this.timeHoursPlayed = 0;
        this.timeDaysPlayed = 0;
        this.sceneToLoad = 1;
        this.positionSpawn = new Vector2(0f,0f);
        this.inventory = null;
        this.chestItems = null;
    }
}

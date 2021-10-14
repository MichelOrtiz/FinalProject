using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorldState 
{
    public string Tag;
    public int id;
    public bool state;
    public WorldState(){
        Tag = "Default";
        id = 5471460;
        state = false;
    }
}

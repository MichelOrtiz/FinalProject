﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    
    [TextArea(5, 10)] public string[] sentences;
    public Dialogue(){
        name = "Empty";
        string[] s = {"Hola","Peter"};
        sentences = s;
    }
}
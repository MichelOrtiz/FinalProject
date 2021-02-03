using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int capacidad = 5;
    private void Awake() {
        if(instance!=null){
            Debug.Log("HOW!!!");
            return;
        }
        instance = this;
    }
    public List<Item> items = new List<Item>();

    public void Add(Item item){
        items.Add(item);
    }
    public void Remove(Item item){
        items.Remove(item);
    }
}

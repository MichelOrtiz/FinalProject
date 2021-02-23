using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public Item[] hotkey;
    public int capacidad = 2;
    private void Awake() {
        if(instance!=null){
            Debug.Log("HOW!!!");
            return;
        }
        instance = this;
    }
    private void Start() {
        hotkey = new Item[4];
    }
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(hotkey[0]!=null){
               hotkey[0].Use();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(hotkey[1]!=null){
               hotkey[1].Use();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            if(hotkey[2]!=null){
               hotkey[2].Use();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            if(hotkey[3]!=null){
               hotkey[3].Use();
            }
        }
    }
    public bool Add(Item item){  
        if(items.Count<capacidad){
            items.Add(item);
            if(onItemChangedCallBack != null){
                onItemChangedCallBack.Invoke();
            }
            return true;
        }
        else{
            Debug.Log("No hay espacio");
            return false;
        }
    }
    
    public void Remove(Item item){
        items.Remove(item);
        if(onItemChangedCallBack != null){
            onItemChangedCallBack.Invoke();
        }
    }
    public void SwapItems(int originIndex, int destinationIndex){
        Item origin = items.ToArray()[originIndex];
        Item destination = items.ToArray()[destinationIndex];

        items.Insert(destinationIndex,origin);
        items.RemoveAt(destinationIndex+1);
        items.Insert(originIndex,destination);
        items.RemoveAt(originIndex+1);
        if(onItemChangedCallBack != null){
            onItemChangedCallBack.Invoke();
        }
    }
    
}

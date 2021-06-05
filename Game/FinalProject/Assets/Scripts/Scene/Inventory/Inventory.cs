using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static int money;
    public Text moneyText;
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    //public UnityEngine.Object[] hotbar1;
    public int capacidad;
    private void Awake() {
        if(instance!=null){
            Debug.Log("HOW!!!");
            return;
        }
        instance = this;
    }
    private void Start() {
        if(SaveFilesManager.instance.currentSaveSlot != null){
            items.Clear();
            for(int i=0;i<SaveFilesManager.instance.currentSaveSlot.inventoy.Length;i++){
                Add(SaveFilesManager.instance.currentSaveSlot.inventoy[i]);
            }
        }
    }
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P)){
            AddMoney(1);
        }
        if(Input.GetKeyDown(KeyCode.O)){
            RemoveMoney(1);
        }
        
    }
    
    public void AddMoney(int i){
        money+=i;
        UpdateMoneyUI();
        return;
    }
    public bool RemoveMoney(int i){
        if(money<i){
            Debug.Log("No hay suficiente dinero");
            return false;
        }
        money-=i;
        UpdateMoneyUI();
        return true;
    }
    public void UpdateMoneyUI(){
        moneyText.text = "Dinero:"+money+"G";
        return;
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
        Item origin = items[originIndex];
        Item destination = items[destinationIndex];

        items.Insert(destinationIndex,origin);
        items.RemoveAt(destinationIndex+1);
        items.Insert(originIndex,destination);
        items.RemoveAt(originIndex+1);
        if(onItemChangedCallBack != null){
            onItemChangedCallBack.Invoke();
        }
    }
    public Item GetRandomEdibleItem()
    {
        Item item = new Item();
        if (items.Count != 0)
        {
            item = items[RandomGenerator.NewRandom(0, items.Count -1)];
            return item;
        }
        return null;
    }
}

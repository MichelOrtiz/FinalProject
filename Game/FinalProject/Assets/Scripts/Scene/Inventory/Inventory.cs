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

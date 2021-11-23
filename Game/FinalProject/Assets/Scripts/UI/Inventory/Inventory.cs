using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private void Awake() {
        if(instance!=null){
            Debug.Log("Sadly only one can exist");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private int money;
    public TextMeshProUGUI moneyText;
    public List<Item> items = new List<Item>();
    public int capacidad;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    
    private void Start() {
        items = new List<Item>();
        LoadSaveData();
    }
    public void LoadSaveData(){
        if(SaveFilesManager.instance!= null && SaveFilesManager.instance.currentSaveSlot != null){
            capacidad = SaveFilesManager.instance.currentSaveSlot.capacidad;
            SetMoney(SaveFilesManager.instance.currentSaveSlot.money);
            items.Clear();
            if(SaveFilesManager.instance.currentSaveSlot.inventory != null){
                for(int i=0;i<SaveFilesManager.instance.currentSaveSlot.inventory.Length;i++){
                    if(SaveFilesManager.instance.currentSaveSlot.inventory[i] == null){
                        Debug.Log("Corrupted data");
                    }else{
                        SaveFilesManager.instance.currentSaveSlot.inventory[i].ResetValues();
                        Add(SaveFilesManager.instance.currentSaveSlot.inventory[i]);
                    }
                }
            }
            
        }
        HotbarUI.instance.UpdateHotbar0UI();
        EquipmentManager.instance.UpdateUI();
    }
    private void Update() 
    {
        //Cheats
        /*if(Input.GetKeyDown(KeyCode.P)){
            AddMoney(1);
        }
        if(Input.GetKeyDown(KeyCode.O)){
            RemoveMoney(1);
        }
        */
        
    }
    public int GetMoney(){
        return money;
    }

    public void SetMoney(int money){
        if (money < 0)
        {
            money = 0;
        }
        this.money = money;
        UpdateMoneyUI();
    }
    public void AddMoney(int i){
        money+=i;
        UpdateMoneyUI();
        return;
    }
    public bool RemoveMoney(int i){
        if(money < i){
            Debug.Log("No hay suficiente dinero");
            return false;
        }
        money -= i;
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
        if(item.isInCooldown){
            StartCoroutine(item.UndoCooldown());
        }
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
        Item item;
        if (items.Count != 0)
        {
            item = items[RandomGenerator.NewRandom(0, items.Count -1)];
            return item;
        }
        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    private void Awake() {
        if(instance!=null){
            Debug.Log("Bad");
            return;
        }
        instance = this;
    }
    public delegate void PassiveEquipment();
    public PassiveEquipment equipmentRutines;
    [SerializeField] Transform holderSlots;
    public Equipment[] currentEquipment;
    public HotbarSlotEq[] slotsEquipment {get => holderSlots.GetComponentsInChildren<HotbarSlotEq>();}
    public ItemSlot equipmentQ;
    public ItemSlot equipmentR;
    
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentPosition)).Length;
        currentEquipment = new Equipment[numSlots];
        Inventory.instance.onItemChangedCallBack += UpdateUI;
        UpdateUI();
    }

    public void Equip(Equipment newItem){
        int slotIndex = (int) newItem.equipmentSlot;
        Unequip(slotIndex);
        currentEquipment[slotIndex] = newItem;
        currentEquipment[slotIndex].StartEquip();
        if(newItem.isPasive){  
            equipmentRutines -= currentEquipment[slotIndex].Rutina;
            equipmentRutines += currentEquipment[slotIndex].Rutina;
        }
    }

    public void Unequip(int slotIndex){
        if(currentEquipment[slotIndex]!=null){ 
            if(currentEquipment[slotIndex].isPasive){  
                equipmentRutines -= currentEquipment[slotIndex].Rutina;
            }
            currentEquipment[slotIndex].EndEquip();
            currentEquipment[slotIndex] = null;
        }
    }
    public void UnequipAll(){
        for(int i=0;i<currentEquipment.Length;i++){
            equipmentRutines -= currentEquipment[i].Rutina;
            Unequip(i);
        }
    }

    void Update()
    {
        if(equipmentRutines != null){
            //Hagan efecto las rutinas de habilidades pasivas 
            equipmentRutines.Invoke();
        }

        //Activar cosas de la hotbar Q - R
        if(Input.GetKeyDown(KeyCode.Q)){
            if(equipmentQ.GetItem() != null){
                equipmentQ.GetItem().Use();
                if(!Inventory.instance.items.Contains(equipmentQ.GetItem())){
                    equipmentQ.ClearSlot();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.R)){
            if(equipmentR.GetItem() != null){
                equipmentR.GetItem().Use();
                if(!Inventory.instance.items.Contains(equipmentR.GetItem())){
                    equipmentR.ClearSlot();
                }
            }
        }
    }
    public Equipment[] GetCurrentEquipment(){
        return currentEquipment;
    }
    public void UpdateUI(){
        foreach(HotbarSlotEq slot in slotsEquipment){
            if(!Inventory.instance.items.Contains(slot.GetItem())){
                slot.SetItem(null);
            }
        }
    }
}

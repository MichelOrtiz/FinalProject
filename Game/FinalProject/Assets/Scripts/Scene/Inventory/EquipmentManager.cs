using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public delegate void OnEquipmentChanged(Equipment newItem,Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    private void Awake() {
        if(instance!=null){
            Debug.Log("Bad");
            return;
        }
        instance = this;
    }
    
    Equipment[] currentEquipment;
    // Start is called before the first frame update
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem){
        int slotIndex = (int) newItem.equipmentSlot;
        Unequip(slotIndex);
        currentEquipment[slotIndex] = newItem;
        
    }

    public void Unequip(int slotIndex){
        if(currentEquipment[slotIndex]!=null){
            Equipment oldItem = currentEquipment[slotIndex];
            Inventory.instance.Add(oldItem);
            currentEquipment[slotIndex] = null;
        }
    }
    public void UnequipAll(){
        for(int i=0;i<currentEquipment.Length;i++){
            Unequip(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){

        }
        if(Input.GetKeyDown(KeyCode.E)){
            UnequipAll();
        }
    }
}

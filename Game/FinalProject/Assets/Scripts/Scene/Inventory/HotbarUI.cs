using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public static HotbarUI instance;
    Inventory inventory;
    InventoryUI inventoryUI;
    public Transform itemsParentHotbar0;
    HotbarSlot[] slotsHotbar0;
    private void Awake() {
        if(instance!=null){
            Debug.Log("NANI!!!");
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventoryUI = InventoryUI.instance;
        slotsHotbar0 = itemsParentHotbar0.GetComponentsInChildren<HotbarSlot>(); 
        for(int i=0;i<slotsHotbar0.Length;i++){
            slotsHotbar0[i].SetIndex(i);
            slotsHotbar0[i].SetItem(null);
        }
        inventory.onItemChangedCallBack += UpdateHotbar0UI;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("HotbarObj0")){
            if(slotsHotbar0[0].GetItem()!=null){
                slotsHotbar0[0].UseItem();
            }
        }
        if(Input.GetButtonDown("HotbarObj1")){
            if(slotsHotbar0[1].GetItem()!=null){
                slotsHotbar0[1].UseItem();
            }
        }
        if(Input.GetButtonDown("HotbarObj2")){
            if(slotsHotbar0[2].GetItem()!=null){
                slotsHotbar0[2].UseItem();
            }
        }
        if(Input.GetButtonDown("HotbarObj3")){
            if(slotsHotbar0[2].GetItem()!=null){
                slotsHotbar0[2].UseItem();
            }
        }
        if(Input.GetButtonDown("HotbarObj4")){
            if(slotsHotbar0[3].GetItem()!=null){
                slotsHotbar0[3].UseItem();
            }
        }
    }
     public void UpdateHotbar0UI(){
        for(int i=0;i<slotsHotbar0.Length;i++)
        {
            if(!inventory.items.Contains(slotsHotbar0[i].GetItem())){
                slotsHotbar0[i].SetItem(null);
            }
            else{

            }
        }
    }
    
}

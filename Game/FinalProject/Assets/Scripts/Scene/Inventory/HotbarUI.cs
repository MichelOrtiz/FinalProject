using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    private PlayerInputs inputs;
    public static HotbarUI instance;
    Inventory inventory;
    InventoryUI inventoryUI;
    public Transform itemsParentHotbar0;
    public HotbarSlot[] slotsHotbar0;
    [HideInInspector] public float timeKeyPressed;
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
        timeKeyPressed=0;
        inventory = Inventory.instance;
        inventoryUI = InventoryUI.instance;
        slotsHotbar0 = itemsParentHotbar0.GetComponentsInChildren<HotbarSlot>(); 
        for(int i=0;i<slotsHotbar0.Length;i++){
            slotsHotbar0[i].SetIndex(i);
            slotsHotbar0[i].SetItem(null);
        }
        inventory.onItemChangedCallBack += UpdateHotbar0UI;
        inputs = PlayerManager.instance.inputs;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.isCaptured)
        {
            timeKeyPressed = 0f;
            return;
        }
        if(inputs == null){
            inputs = PlayerManager.instance.inputs;
        }
        if(timeKeyPressed > 0.5){
            GunProjectile.instance.StartAiming();
        }
        if(inputs.ItemHotbarUp[0] && slotsHotbar0[0].GetItem()!=null){
            if(timeKeyPressed <= 0.5){
                slotsHotbar0[0].UseItem();
            }
            if(timeKeyPressed > 0.5){
                GunProjectile.instance.StopAiming();
                GunProjectile.instance.ShotObject(slotsHotbar0[0].GetItem());
                slotsHotbar0[0].RemoveItem();
            } 
            timeKeyPressed = 0;
        }
        if(inputs.ItemHotbarUp[1] && slotsHotbar0[1].GetItem()!=null){
            if(timeKeyPressed <= 0.5){
                slotsHotbar0[1].UseItem();
            }
            if(timeKeyPressed > 0.5){
                GunProjectile.instance.ShotObject(slotsHotbar0[1].GetItem());
                GunProjectile.instance.StopAiming();
                slotsHotbar0[1].RemoveItem();
            }
            timeKeyPressed = 0;
        }
        if(inputs.ItemHotbarUp[2] && slotsHotbar0[2].GetItem()!=null){
            if(timeKeyPressed <= 0.5){
                slotsHotbar0[2].UseItem();
            }
            if(timeKeyPressed > 0.5){
                GunProjectile.instance.StopAiming();
                GunProjectile.instance.ShotObject(slotsHotbar0[2].GetItem());
                slotsHotbar0[2].RemoveItem();
            }
            timeKeyPressed = 0;
        }
        if(inputs.ItemHotbarUp[3] && slotsHotbar0[3].GetItem()!=null){
            if(timeKeyPressed <= 0.5){
                slotsHotbar0[3].UseItem();
            }
            if(timeKeyPressed > 0.5){
                GunProjectile.instance.ShotObject(slotsHotbar0[3].GetItem());
                GunProjectile.instance.StopAiming();
                slotsHotbar0[3].RemoveItem();
            }
            timeKeyPressed = 0;
        }
        if(inputs.ItemHotbarUp[4] && slotsHotbar0[4].GetItem()!=null){
            if(timeKeyPressed <= 0.5){
                slotsHotbar0[4].UseItem();
            }
            if(timeKeyPressed > 0.5){
                GunProjectile.instance.ShotObject(slotsHotbar0[4].GetItem());
                GunProjectile.instance.StopAiming();
                slotsHotbar0[4].RemoveItem();
            }
            timeKeyPressed = 0;
        }


        if(inputs.ItemHotbarDown[0] && slotsHotbar0[0].GetItem()!=null){
            //Debug.Log("timeKey1Pressed:" + timeKeyPressed);
            timeKeyPressed += Time.deltaTime;
        }
        if(inputs.ItemHotbarDown[1] && slotsHotbar0[1].GetItem()!=null){
            //Debug.Log("timeKey2Pressed:" + timeKeyPressed);
            timeKeyPressed += Time.deltaTime;
        }
        if(inputs.ItemHotbarDown[2] && slotsHotbar0[2].GetItem()!=null){
            //Debug.Log("timeKe3yPressed:" + timeKeyPressed);
            timeKeyPressed += Time.deltaTime;
        }
        if(inputs.ItemHotbarDown[3] && slotsHotbar0[3].GetItem()!=null){
            //Debug.Log("timeKey4Pressed:" + timeKeyPressed);
            timeKeyPressed += Time.deltaTime;
        }
        if(inputs.ItemHotbarDown[4] && slotsHotbar0[4].GetItem()!=null){
            //Debug.Log("timeKey5Pressed:" + timeKeyPressed);
            timeKeyPressed += Time.deltaTime;
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

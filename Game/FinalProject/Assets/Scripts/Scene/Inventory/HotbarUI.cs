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
        if (!inputs.enabled)
        {
            timeKeyPressed = 0f;
            return;
        }
        for(int i = 0; i < slotsHotbar0.Length; i++){
            LaunchingObj(i);
        }
        
    }
     public void UpdateHotbar0UI(){
        for(int i=0;i<slotsHotbar0.Length;i++)
        {
            if(!inventory.items.Contains(slotsHotbar0[i].GetItem())){
                slotsHotbar0[i].SetItem(null);
            }
        }
    }
    public void LaunchingObj(int i){
        bool isValidToLaunch = slotsHotbar0[i].GetItem()!=null && slotsHotbar0[i].GetItem().type == Item.ItemType.Consumible;
        if(timeKeyPressed > 0.5){
            GunProjectile.instance.StartAiming();
        }
        if(inputs.ItemHotbarUp[i] && isValidToLaunch){
            if(timeKeyPressed <= 0.5){
                slotsHotbar0[i].UseItem();
            }
            if(timeKeyPressed > 0.5){
                GunProjectile.instance.ShotObject(slotsHotbar0[i].GetItem());
                GunProjectile.instance.StopAiming();
                slotsHotbar0[i].RemoveItem();
            }
            timeKeyPressed = 0;
        }
        if(inputs.ItemHotbarDown[i] && isValidToLaunch){
            //Debug.Log("timeKey1Pressed:" + timeKeyPressed);
            timeKeyPressed += Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    public int movementX {get;set;}
    public int movementY {get;set;}
    public bool jump { get; set; }
    public bool OpenInventory { get; set; }
    public bool[] ItemHotbarUp = new bool[5];
    public bool[] ItemHotbarDown = new bool[5];
    public bool[] EquipmentHotbar = new bool[5];

    public Action MovedRight;
    public Action MovedLeft;
    public Action MovedUp;
    public Action MovedDown;
    public Action Jump;


    void Update()
    {
        #region Right Left Up Dowm
        if(Input.GetButton("MovementRight")){
            movementX=1;
            MovedRight?.Invoke();
        }
        else if(Input.GetButton("MovementLeft")){
            movementX=-1;
            MovedLeft?.Invoke();
        }
        else{
            movementX=0;
        }
        if(Input.GetButton("MovementUp")){
            movementY=1;
            MovedUp?.Invoke();
        }
        else if(Input.GetButton("MovementDown")){
            movementY=-1;
            MovedDown?.Invoke();
        }
        else{
            movementY=0;
        }
        #endregion

        jump=Input.GetButton("Jump");
        if (jump)
        {
            Jump?.Invoke();
        }

        OpenInventory=Input.GetButtonDown("Inventory");

        HotbarInputs();
    }
    public void HotbarInputs(){
        if(Input.GetButton("HotbarObj0")){
            ItemHotbarDown[0]=true;
        }else{
            ItemHotbarDown[0]=false;
        }
        if(Input.GetButtonUp("HotbarObj0")){
            ItemHotbarUp[0]=true;
        }else{
            ItemHotbarUp[0]=false;
        }

        if(Input.GetButton("HotbarObj1")){
            ItemHotbarDown[1]=true;
        }else{
            ItemHotbarDown[1]=false;
        }
        if(Input.GetButtonUp("HotbarObj1")){
            ItemHotbarUp[1]=true;
        }else{
            ItemHotbarUp[1]=false;
        }

        if(Input.GetButton("HotbarObj2")){
            ItemHotbarDown[2]=true;
        }else{
            ItemHotbarDown[2]=false;
        }
        if(Input.GetButtonUp("HotbarObj2")){
            ItemHotbarUp[2]=true;
        }else{
            ItemHotbarUp[2]=false;
        }

        if(Input.GetButton("HotbarObj3")){
            ItemHotbarDown[3]=true;
        }else{
            ItemHotbarDown[3]=false;
        }
        if(Input.GetButtonUp("HotbarObj3")){
            ItemHotbarUp[3]=true;
        }else{
            ItemHotbarUp[3]=false;
        }

        if(Input.GetButton("HotbarObj4")){
            ItemHotbarDown[4]=true;
        }else{
            ItemHotbarDown[4]=false;
        }
        if(Input.GetButtonUp("HotbarObj4")){
            ItemHotbarUp[4]=true;
        }else{
            ItemHotbarUp[4]=false;
        }
        
    }
    private void OnDisable() {
        movementX=0;
        movementY=0;
        jump=false;
        OpenInventory = false;
    }
}

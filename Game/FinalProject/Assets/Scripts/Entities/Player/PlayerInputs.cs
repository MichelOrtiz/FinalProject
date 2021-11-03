using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    #region Inputs binding
    public Dictionary<string, KeyCode> controlBinds {get;set;}
    #endregion
    public int movementX {get;set;}
    public int movementY {get;set;}
    public bool jump { get; set; }
    public bool ctrlLeft { get; set; }
    public bool OpenInventory { get; set; }
    public bool OpenMiniMap { get; set; }
    public bool OpenMap { get; set; }
    public bool OpenAbilites {get;set;}
    public bool[] ItemHotbarUp = new bool[5];
    public bool[] ItemHotbarDown = new bool[5];
    public bool[] EquipmentHotbar = new bool[5];
    public List<KeyCode> skillHotkeys {
        get{
            List<KeyCode> keys = new List<KeyCode>();
            foreach(String k in controlBinds.Keys){
                if(k.StartsWith("SKILL")){
                    keys.Add(controlBinds[k]);
                }
            }
            return keys;
        }
    }
    public float intputLag {get;set;}
    public const float defaultInputLag = 0;
    public KeyCode Pause;
    public KeyCode Map;

    public Action MovedRight = delegate(){PlayerManager.instance.inputs.movementX=1;};
    public Action MovedLeft = delegate(){PlayerManager.instance.inputs.movementX=-1;};
    public Action MovedUp = delegate(){PlayerManager.instance.inputs.movementY=1;};
    public Action MovedDown = delegate(){PlayerManager.instance.inputs.movementY=-1;};

    public Action Interact = delegate(){};

    public Action Jump;
    
    bool checkLag;
    private void Start() {
        intputLag = defaultInputLag;  
        checkLag = false;
    }
    
    void Update()
    {
        if(!enabled){ 
            movementX=0;
            movementY=0;
            jump=false;
            return;
        }
        //if (controlBinds == null) return;
        #region Right Left Up Dowm
        if(Input.GetKey(controlBinds["MOVERIGHT"])){
            if(intputLag > 0){   
                StartCoroutine(ApplyInputLag(MovedRight));
            }else{
                MovedRight?.Invoke();
            }
        }
        else if(Input.GetKey(controlBinds["MOVELEFT"])){
            if(intputLag > 0){
                StartCoroutine(ApplyInputLag(MovedLeft));
            }else{
                MovedLeft?.Invoke();
            }
            //movementX=-1;
        }
        else{
            movementX=0;
        }
        if(Input.GetKey(controlBinds["MOVEUP"])){
            if(intputLag > 0){
                StartCoroutine(ApplyInputLag(MovedUp));
            }else{
                MovedUp?.Invoke();
            }  
            //movementY=1;
        }
        else if(Input.GetKey(controlBinds["MOVEDOWN"])){
            if(intputLag > 0){
                StartCoroutine(ApplyInputLag(MovedDown));
            }else{
                MovedDown?.Invoke();
            }
            
            //movementY=-1;
        }
        else{
            movementY=0;
        }
        #endregion
        if(Input.GetKey(controlBinds["MOVEJUMP"])){
            //StartCoroutine(ApplyInputLag());
            jump=true;
            Jump?.Invoke();
        }
        else{
            jump=false;
        }
        

        if (Input.GetKeyDown(controlBinds["MENUINTERACTION"]))
        {
            Interact?.Invoke();
        }
        
        ctrlLeft = Input.GetKey(controlBinds["MENUFASTASSIGN"]);
        OpenInventory=Input.GetKeyDown(controlBinds["MENUINVENTORY"]);
        OpenMiniMap=Input.GetKeyDown(controlBinds["MENUMINIMAP"]);
        OpenMap=Input.GetKeyDown(controlBinds["MENUMAP"]);
        OpenAbilites = Input.GetKeyDown(controlBinds["MENUHABILIDAD"]);
        

        HotbarInputs();
        
    }

    public void HotbarInputs(){
        if(Input.GetKey(controlBinds["FOOD1"])){
            ItemHotbarDown[0]=true;
        }else{
            ItemHotbarDown[0]=false;
        }
        if(Input.GetKeyUp(controlBinds["FOOD1"])){
            ItemHotbarUp[0]=true;
        }else{
            ItemHotbarUp[0]=false;
        }

        if(Input.GetKey(controlBinds["FOOD2"])){
            
            ItemHotbarDown[1]=true;
        }else{
            ItemHotbarDown[1]=false;
        }
        if(Input.GetKeyUp(controlBinds["FOOD2"])){
            ItemHotbarUp[1]=true;
        }else{
            ItemHotbarUp[1]=false;
        }

        if(Input.GetKey(controlBinds["FOOD3"])){
            
            ItemHotbarDown[2]=true;
        }else{
            ItemHotbarDown[2]=false;
        }
        if(Input.GetKeyUp(controlBinds["FOOD3"])){
            ItemHotbarUp[2]=true;
        }else{
            ItemHotbarUp[2]=false;
        }

        if(Input.GetKey(controlBinds["FOOD4"])){
            
            ItemHotbarDown[3]=true;
        }else{
            ItemHotbarDown[3]=false;
        }
        if(Input.GetKeyUp(controlBinds["FOOD4"])){
            ItemHotbarUp[3]=true;
        }else{
            ItemHotbarUp[3]=false;
        }

        if(Input.GetKey(controlBinds["FOOD5"])){
            
            ItemHotbarDown[4]=true;
        }else{
            ItemHotbarDown[4]=false;
        }
        if(Input.GetKeyUp(controlBinds["FOOD5"])){
            ItemHotbarUp[4]=true;
        }else{
            ItemHotbarUp[4]=false;
        }
        
    }
    
    public void ResetHotbarInputs(){
        for (int i = 0; i < ItemHotbarUp.Length; i++)
        {
            ItemHotbarUp[i] = false;
            ItemHotbarDown[i] = false;
            
        }
        PlayerManager.instance.isAiming = false;
    }
    IEnumerator ApplyInputLag(Action doLast){
        yield return new WaitForSeconds(intputLag);
        doLast();
    }
}

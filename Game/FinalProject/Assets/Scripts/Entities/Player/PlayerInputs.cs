using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public int movementX {get;set;}
    public int movementY {get;set;}
    public bool jump { get; set; }
    void Update()
    {
        #region Right Left Up Dowm
        if(Input.GetButton("MovementRight")){
            movementX=1;
        }
        else if(Input.GetButton("MovementLeft")){
            movementX=-1;
        }
        else{
            movementX=0;
        }
        if(Input.GetButton("MovementUp")){
            movementY=1;
        }
        else if(Input.GetButton("MovementDown")){
            movementY=-1;
        }
        else{
            movementY=0;
        }
        #endregion

        if(Input.GetButton("Jump")){
            jump=true;
        }
        else{
            jump=false;
        }
    }
    private void OnDisable() {
        movementX=0;
        movementY=0;
        jump=false;
    }
}

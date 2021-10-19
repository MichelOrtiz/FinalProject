using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/KeyPressed")]
public class CPressedKey : InterCondition
{
    [SerializeField] string keyToPress;
    bool wasPressed = false;
    protected override bool checkIsDone()
    {
        PlayerInputs inputs = PlayerManager.instance.inputs;
        if(!inputs.controlBinds.ContainsKey(keyToPress)){
            Debug.Log(keyToPress + " no esxiste en controlBinds");
            return false;
        }
        if(Input.GetKeyDown(inputs.controlBinds[keyToPress])){
            wasPressed = true;
        }
        return wasPressed;
    }
    public override void RestardValues()
    {
        wasPressed = false;
    }
}

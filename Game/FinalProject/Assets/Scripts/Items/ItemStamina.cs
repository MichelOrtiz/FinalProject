using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/ItemStamina")]
public class ItemStamina : Item
{
    public int staminaGain; 
    public override void Use()
    {
        if(staminaGain < 0){
            PlayerManager.instance.TakeTirement(-staminaGain);
        }else{
            PlayerManager.instance.RegenStamina(staminaGain);
        }
        base.Use();
    }
}

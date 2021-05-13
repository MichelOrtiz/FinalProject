using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment",menuName = "Inventory/New Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public bool isPasive = true;
    public bool activate { get; set; }
    private float cooldown;
    private float currentCooldown;
    public override void Use()
    {
        EquipmentManager.instance.Equip(this);
    }
    public virtual void rutine(){
        //Cosa que haga...
    }

}

public enum EquipmentSlot {Head, Chest, Legs, Hands, Feet}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Item
{
    public EquipmentPosition equipmentSlot;
    public bool isPasive = true;
    public bool activate { get; set; }
    private float cooldown;
    private float currentCooldown = 0;
    protected PlayerManager player;
    public override void Use()
    {
        if(isPasive){
            //solo se equipan los equipamientos pasivos y los activos solo ejecutan su rutina...
            if(EquipmentManager.instance.GetCurrentEquipment()[(int)equipmentSlot] == this){
                EquipmentManager.instance.Unequip((int) equipmentSlot);
                return;
            }   
            EquipmentManager.instance.Equip(this);
        }else{
            Rutina();
        }
    }
    public override void RemoveFromInventory()
    {
        Debug.Log("No puedes solo tirar una pieza de equipamiento en el suelo!!");
        return;
    }
    public virtual void Rutina(){
        if(cooldown != 0){
            if(currentCooldown > 0){
                currentCooldown -= Time.deltaTime;
                return;
            }
        }
    }
    public virtual void StartEquip(){
        player = PlayerManager.instance;
    }
    public virtual void EndEquip(){

    }

}

public enum EquipmentPosition {Head, Chest, Legs, Hands, Feet}

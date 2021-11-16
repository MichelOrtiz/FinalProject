using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Item", menuName = "Equipment/Jetpack")]
public class Jetpack : Equipment
{
    Vector2 empuje;
    public int uses;
    [SerializeField] int maxUses = 5;
    [SerializeField] const float defaultForce = 10f;
    [SerializeField] float speed;
    public override void Rutina(){
        if(player.isGrounded){
            uses = maxUses;
        }
    }

    public override void StartEquip(){
        EquipmentManager.instance.equipmentRutines -= Rutina;
        EquipmentManager.instance.equipmentRutines += Rutina;
    }
    public override void EndEquip(){
        EquipmentManager.instance.equipmentRutines -= Rutina;
    }
    public void RestablecerUsos()
    {
        if(PlayerManager.instance.isGrounded){
            uses = maxUses;
        }
    }

    public override void Use()
    {
        if(EquipmentManager.instance.GetCurrentEquipment()[(int)equipmentSlot] != this){
            EquipmentManager.instance.Unequip((int) equipmentSlot);
            EquipmentManager.instance.Equip(this);
        }  
        Impulso();
    }
    void Impulso(){
        if(uses < 0) return;
        uses --;
        Rigidbody2D body = PlayerManager.instance.GetComponent<Rigidbody2D>();
        //Debug.Log("Usando " + this.name);
        empuje = new Vector2(0f,defaultForce);
        //body.gravityScale *= -1;
        body.velocity = new Vector2(0f,0f);
        body.AddForce(empuje * speed,ForceMode2D.Impulse);
        PlayerManager.instance.isJumping = true;
    }
}

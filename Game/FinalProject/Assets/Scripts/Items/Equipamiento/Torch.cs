using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Equipment", menuName = "Equipment/Linterna")]
public class Torch : Equipment
{
    public override void StartEquip(){
        base.StartEquip();
        GunProjectile.instance.linterna.SetActive(true);
    }
    public override void EndEquip()
    {
        base.EndEquip();
        GunProjectile.instance.linterna.SetActive(false);
    }
}

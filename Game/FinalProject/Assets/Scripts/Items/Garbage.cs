using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="New Trash", menuName = "Inventory/Trash")]
public class Garbage : Item
{
    public override void Use()
    {
        //No hace nada
    }
    public override void RemoveFromInventory()
    {
        //Solo se puede quitar mediante una interaccion en el bazar
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "new Condition", menuName = "Interaction/Conditions/!ExistItem")]
public class CNotExistItem : InterCondition
{
    [SerializeField] Item searchItem;
    protected override bool checkIsDone()
    {
        List<Inter> itemsInScene = ScenesManagers.FindObjectsOfType<Inter>().ToList<Inter>();
        List<Item> itemsInInv = Inventory.instance.items;
        List<Item> itemsInCof = Cofre.instance.savedItems;
        if(itemsInInv.Count > 0){
            if(itemsInInv.Contains(searchItem)) return false;
        }
        if(itemsInCof.Count > 0){
            if(itemsInCof.Contains(searchItem)) return false;
        }
        if(itemsInScene.Count > 0){
            foreach(Inter pickUp in itemsInScene){
                if(pickUp.item == searchItem) return false;
            }
        }
        return true;

    }
}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType{
        Consumible,
        Equipable,
        Basura,
        Mision
    }
    public ItemType type;
    public string nombre = "Nombre objeto";
    [TextArea(3, 10)]
    public string descripcion = "Un objeto objetuoso";
    public Sprite icon = null;
    public bool isConsumable = true;
    public bool isInCooldown = false;
    public float cooldownTime = 3.5f;
    [HideInInspector] public float currentCooldownTime;
    public virtual void Use(){
        if(!isConsumable)return;
        if(isInCooldown){
            Debug.Log("Objeto en cooldown");
            return;
        }
        RemoveFromInventory();
    }
    public virtual void RemoveFromInventory(){
        Inventory.instance.Remove(this);
    }
    public IEnumerator UndoCooldown(){
        if(cooldownTime > 0){
            isInCooldown = true;
            currentCooldownTime = 1f;
            while(currentCooldownTime > 0){
                currentCooldownTime -= Time.deltaTime / cooldownTime;
                yield return null;   
            }
            isInCooldown = false;
        }
        
    }
}

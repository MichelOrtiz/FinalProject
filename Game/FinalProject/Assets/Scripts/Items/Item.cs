using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string nombre = "Nombre objeto";
    [TextArea(3, 10)]
    public string descripcion = "Un objeto objetuoso";

    public int staminaGain; 
    public Sprite icon = null;
    public bool isDefault = false;

    public virtual void Use(){
        //Debug.Log("Usando "+nombre);
        if(staminaGain<0){
            PlayerManager.instance.TakeTirement(staminaGain);
        }else{
            PlayerManager.instance.RegenStamina(staminaGain);
        }
        
        RemoveFromInventory();
    }
    public void RemoveFromInventory(){
        Inventory.instance.Remove(this);
    }
}

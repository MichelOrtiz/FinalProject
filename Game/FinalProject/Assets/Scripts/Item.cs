using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string nombre = "Nombre objeto";
    public string descripcion = "Un objeto objetuoso";
    public Sprite icon = null;
    public bool isDefault = false;
}

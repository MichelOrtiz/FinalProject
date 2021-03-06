using UnityEngine;
[CreateAssetMenu(fileName="New ItemParticlesB", menuName = "Inventory/Item ParticlesB")]
public class ParticlesItem : Item
{
    public Sprite particleImg;
    
    public override void Use(){
        base.Use();
        InventoryUI.instance.ForceCloseUI();
        ParticleSystemController.instance.Play();
    }
}

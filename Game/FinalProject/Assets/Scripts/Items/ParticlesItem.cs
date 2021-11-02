using UnityEngine;
[CreateAssetMenu(fileName="New ItemParticlesB", menuName = "Inventory/Item ParticlesB")]
public class ParticlesItem : Item
{
    public Sprite particleImg;
    
    public override void Use(){
        if(isInCooldown){
            Debug.Log("Objeto en cooldown");
            return;
        }
        base.Use();
        InventoryUI.instance.UI.SetActive(false);
        ParticleSystemController.instance.Play();
    }
}

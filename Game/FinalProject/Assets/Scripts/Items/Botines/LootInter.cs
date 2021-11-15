using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInter : MonoBehaviour
{
    [SerializeField] private LootChest loot;
    [SerializeField] private SpriteRenderer imagen;
    [SerializeField] GameObject interSign;
    public float radius = 3f;
    private PlayerManager player;
    private void Start() {
        player = PlayerManager.instance;
        imagen.sprite = loot.sprite;
        loot.SetSpawnPoint(transform.position);
    }
    private void Update() {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(distance <= radius){
            PlayerManager.instance.inputs.Interact -= Open;
            PlayerManager.instance.inputs.Interact += Open;
            interSign?.SetActive(true);
        }else{
            PlayerManager.instance.inputs.Interact -= Open;
            interSign?.SetActive(false);
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Open(){
        loot.SetSpawnPoint(PlayerManager.instance.GetPosition());
        loot.Open();
        PlayerManager.instance.inputs.Interact -= Open;
        Destroy(gameObject);
    }
}

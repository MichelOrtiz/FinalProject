using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInter : MonoBehaviour
{
    [SerializeField] private LootChest loot;
    [SerializeField] private SpriteRenderer imagen;
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
            if(Input.GetKeyDown(KeyCode.E)){
                loot.SetSpawnPoint(player.GetPosition());
                loot.Open();
                Destroy(gameObject);
            }
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter : MonoBehaviour
{
    public float radius = 3f;
    public Item item;
    public Transform player;

    private void Update() {
        float distance = Vector2.Distance(player.position, transform.position);
        if(distance <= radius){
            if(Input.GetKeyDown(KeyCode.E)){
                Debug.Log("Agarrando " + item.nombre);
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

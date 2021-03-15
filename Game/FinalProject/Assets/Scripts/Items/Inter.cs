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
                bool IsPicked = Inventory.instance.Add(item);
                if(IsPicked){
                    Destroy(gameObject);
                }
            }
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Kill MEEEE!");
        Enemy enemigo = other.GetComponent<Enemy>();
        if(enemigo!=null)
        {
            Debug.Log(enemigo.gameObject.name);
            enemigo.ConsumeItem(item);
            Destroy(gameObject);
        }
    }
}
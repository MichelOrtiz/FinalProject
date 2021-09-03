using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjProjectile : MonoBehaviour
{
    public GameObject itemPickPrefab;
    public SpriteRenderer bulletImg;
    private Item item;
    public float speed;
    public Rigidbody2D rigidbody;
    private void Start() {
        rigidbody.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player")){
            //Debug.Log(other.name);
            if(other.CompareTag("Enemy")){
                Enemy enemy = other.transform.parent.GetComponentInChildren<Enemy>();
                if(enemy!=null){
                    Debug.Log("Enemigo consumio objeto proyectil directamente");
                    enemy.ConsumeItem(item);
                    Destroy(gameObject);
                    return;
                }
                ItemGetter getter = other.gameObject.GetComponent<ItemGetter>();
                if(getter!=null){
                    if(getter.GetItem(item)){
                        Debug.Log("Interactuable (ItemGetter) consumio objeto proyectil directamente");
                        Destroy(gameObject);
                        return;
                    }
                }
            }
            Vector3 newPosition = new Vector3 (transform.position.x,transform.position.y + 0.5f,transform.position.z);
            Debug.Log("Generendo objeto en posicion de colision: " + newPosition.ToString());
            GameObject x = Instantiate(itemPickPrefab,newPosition,Quaternion.identity);
            x.GetComponent<Inter>().SetItem(item);
            Destroy(gameObject);
        }
        
    }
    public Item GetItem(){
        return item;
    }
    public void SetItem(Item newItem){
        item = newItem;
        bulletImg.sprite = item.icon;
    }
}

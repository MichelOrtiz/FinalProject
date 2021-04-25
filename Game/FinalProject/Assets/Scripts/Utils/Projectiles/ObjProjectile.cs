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
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                if(enemy!=null){
                    enemy.ConsumeItem(item);
                    Destroy(gameObject);
                    return;
                }
                ItemGetter getter = other.gameObject.GetComponent<ItemGetter>();
                if(getter!=null){
                    if(getter.GetItem(item)){
                        Destroy(gameObject);
                        return;
                    }
                }
            }
            Destroy(gameObject);
            Vector3 newPosition = new Vector3 (transform.position.x,transform.position.y + 0.5f,transform.position.z);
            GameObject x = Instantiate(itemPickPrefab,newPosition,Quaternion.Euler(0f,0f,0f));
            x.GetComponent<Inter>().SetItem(item);
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

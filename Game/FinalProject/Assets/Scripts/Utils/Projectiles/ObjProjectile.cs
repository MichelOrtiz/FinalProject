using System.Collections;
using System.Collections.Generic;
using FinalProject.Assets.Scripts.Utils.Physics;
using UnityEngine;

[RequireComponent(typeof(SomePhysics))]
public class ObjProjectile : MonoBehaviour
{
    public GameObject itemPickPrefab;
    public SpriteRenderer bulletImg;
    private Item item;
    public float speed;
    public Rigidbody2D rigidbody;
    [SerializeField] private CollisionHandler collisionHandler;
    
    private SomePhysics physics;
    public Knockback knockback;
    
    Vector2 direction;
    Transform origin;

    void Awake()
    {
        physics = GetComponent<SomePhysics>();
        collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
    }

    private void Start()
    {
        //rigidbody.velocity = transform.right * speed;
        var shotPos = new Vector2(transform.position.x, transform.position.y);
        var mouseDir = PlayerManager.instance.GetComponentInChildren<MouseDirPointer>().MouseDirection;
        var mouseDirSt = new Vector2(mouseDir.x, mouseDir.y);

        origin = PlayerManager.instance.GetComponentInChildren<GunProjectile>().shotPoint;
        direction =  origin.position + (Vector3) mouseDir;

        physics.StartKnockback(knockback.duration, knockback.force, origin.TransformDirection(-direction/2));
    }

    void Update()
    {
        Debug.DrawLine(origin.position, direction);
    }

    void collisionHandler_EnterContact(GameObject other)
    {
        if (other.CompareTag("Enemy") || GroundChecker.GroundTags.Exists(tg => tg != "Boundary" && tg == other.tag) )
        {
            Debug.Log(other.name);
            physics.StopAllCoroutines();
            Enemy enemy = other.transform?.parent?.GetComponentInChildren<Enemy>();
            if(enemy!=null)
            {
                Debug.Log("Enemigo consumio objeto proyectil directamente");
                enemy.ConsumeItem(item);
                Destroy(gameObject);
                return;
            }
            ItemGetter getter = other.gameObject.GetComponent<ItemGetter>();
            if(getter!=null)
            {
                if(getter.GetItem(item))
                {
                    Debug.Log("Interactuable (ItemGetter) consumio objeto proyectil directamente");
                    Destroy(gameObject);
                    return;
                }
            }
            Vector3 newPosition = new Vector3 (transform.position.x,transform.position.y + 0.5f,transform.position.z);
            Debug.Log("Generendo objeto en posicion de colision: " + newPosition.ToString());
            GameObject x = Instantiate(itemPickPrefab,newPosition,Quaternion.identity);
            x.GetComponent<Inter>().SetItem(item);
            Destroy(gameObject);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other) {
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
        
    }*/
    public Item GetItem(){
        return item;
    }
    public void SetItem(Item newItem){
        item = newItem;
        bulletImg.sprite = item.icon;
    }
}

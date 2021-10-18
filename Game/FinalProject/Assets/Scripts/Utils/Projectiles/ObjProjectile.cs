using System.Collections;
using System.Collections.Generic;
using FinalProject.Assets.Scripts.Utils.Physics;
using UnityEngine;
using System.Threading;

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
        rigidbody.velocity = transform.right * speed;
        /*var shotPos = new Vector2(transform.position.x, transform.position.y);
        var mouseDir = PlayerManager.instance.GetComponentInChildren<MouseDirPointer>().MouseDirection;
        var mouseDirSt = new Vector2(mouseDir.x, mouseDir.y);

        origin = PlayerManager.instance.GetComponentInChildren<GunProjectile>().shotPoint;
        direction =  origin.position + (Vector3) mouseDir;

        physics.StartKnockback(knockback.duration, knockback.force, origin.TransformDirection(-direction/2));*/
    }


    void collisionHandler_EnterContact(GameObject other)
    {
        if (other.CompareTag("Enemy") || GroundChecker.GroundTags.Exists(tg => tg != "Boundary" && tg == other.tag) )
        {
            //Encontre varios casos donde ObjProjectile generaba mas de un objeto PickUp al colisionar con una superficie... esto evitara que eso pase
            Debug.Log(other.name);
            physics.StopAllCoroutines();
            Enemy enemy = other.transform?.parent?.GetComponentInChildren<Enemy>();
            if(enemy!=null)
            {
                Destroy(gameObject);
                Debug.Log("Enemigo consumio objeto proyectil directamente");
                enemy.ConsumeItem(item);
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
            Vector3 newPosition = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
            Debug.Log("Generendo objeto en posicion de colision: " + newPosition.ToString());
            Destroy(gameObject);
            GameObject x = Instantiate(itemPickPrefab,newPosition,Quaternion.identity);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter : MonoBehaviour
{
    public SpriteRenderer imagen;
    public float radius = 3f;
    public string selfTag = "Berry";
    public Item item;
    private PlayerManager player;
    [SerializeField] GameObject interSign;
   
    void Awake()
    {
        tag = selfTag;
    }

    private void Start() {
        player = PlayerManager.instance;
        if (item != null)
        {
            imagen.sprite = item.icon;
            
        }
        
    }
    private void Update() {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(item.type == Item.ItemType.Mision){
            ItemMission itemMission = (ItemMission) item;
            if(Inventory.instance.items.Contains(itemMission) || Cofre.instance.savedItems.Contains(itemMission)){
                Destroy(gameObject);
            }
            if(SaveFilesManager.instance.currentSaveSlot.WorldStates.Exists(x => x.id == itemMission.appearWhen.id)){
                WorldState w = SaveFilesManager.instance.currentSaveSlot.WorldStates.Find(x => x.id == itemMission.appearWhen.id);
                if(!w.state){
                    Destroy(gameObject);
                }
            }else{
                Destroy(gameObject);
            }
        }
        if(distance <= radius){
            player.inputs.Interact -= PickUpObj;
            player.inputs.Interact += PickUpObj;
            interSign?.SetActive(true);
        }else{
            player.inputs.Interact -= PickUpObj;
            interSign?.SetActive(false);
        }
    }
    public void PickUpObj(){
        bool IsPicked = Inventory.instance.Add(item);
        if(IsPicked){
            Debug.Log(item.name + " picked");
            Destroy(gameObject);
        }
        
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    //Estoy pensando en crear una funcion sincrona para evitar que dos cosas sean afectadas por el mismo objeto
    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemigo = other.transform?.parent?.GetComponentInChildren<Enemy>();
        //Debug.Log(other.gameObject);
        if(enemigo!=null)
        {
            enemigo.ConsumeItem(item);
            Destroy(gameObject);
        }
        ItemGetter getter = other.gameObject.GetComponent<ItemGetter>();
        if(getter!=null){
            if(getter.GetItem(item)){
                Debug.Log("ÑOM ÑOM");
                Destroy(gameObject);
                return;
            }
        }
    }
    public void SetItem(Item newItem){
        item = newItem;
        imagen.sprite = item.icon;
    }
    private void OnDestroy() {
        //Es necesario esto o estoy siendo paranoico
        player.inputs.Interact -= PickUpObj;
    }
}

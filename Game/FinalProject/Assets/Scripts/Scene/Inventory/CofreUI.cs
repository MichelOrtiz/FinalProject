using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CofreUI : MonoBehaviour
{
    public static CofreUI instance;
    private void Awake() {
        if(instance!=null){
            Debug.Log("word");
            return;
        }
        instance = this;
    }
    private ItemSlot focusedSlot;
    public Button nextButtonCof;
    public Button prevButtonCof;
    public Button nextButtonInv;
    public Button prevButtonInv;

    public Text description;
    public Text nametxt;
    public Cofre cofre;
    public Inventory inventory;
    public Transform slotsCofUI;
    public Transform slotsInvUI;
    CofreSlot[] slotsCof;
    CofreSlot[] slotsInv;

    public ItemSlot moveItemSlot;
    private int pageCof;
    private int pageInv;

    void Start()
    {
        focusedSlot=null;
        moveItemSlot = null;
        //moveItemIndex=0;
        inventory = Inventory.instance;
        cofre = Cofre.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        cofre.onItemChangedCallBack += UpdateUI;
        slotsCof = slotsCofUI.GetComponentsInChildren<CofreSlot>();
        slotsInv = slotsInvUI.GetComponentsInChildren<CofreSlot>();
        
        pageCof = 0;
        pageInv = 0;
        UpdateUI();
        gameObject.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)){ //cerrar la ventana ... deberia ser con ESC pero la pausa tambien se activa con ESC
            gameObject.SetActive(false);
        }
        if(inventory == null || cofre == null){ //probablemente no es necesario solo soy paranoico
            inventory = Inventory.instance;
            cofre = Cofre.instance;
        }
    }
    public void UpdateUI(){

        //cargar inventario en UI
        //Debug.Log("Cargando objetos en inventario-CofreUI");
        for(int i=0; i<slotsInv.Length;i++){
        slotsInv[i].SetIndex(i+pageInv);
        slotsInv[i].origen = CofreSlot.Holder.Inventario;
        if(i+pageInv < inventory.items.Count){
            slotsInv[i].SetItem(inventory.items[i+pageInv]);
        }else{
            slotsInv[i].ClearSlot();
        }
            
        }
        if(pageInv+10 < inventory.items.Count){
            nextButtonInv.gameObject.SetActive(true);
        }
        else{
            nextButtonInv.gameObject.SetActive(false);
        }
        if(pageInv!=0){
            prevButtonInv.gameObject.SetActive(true);
        }
        else{
            prevButtonInv.gameObject.SetActive(false);
        }

        //cargar cofre en UI
        for(int i=0; i<slotsCof.Length;i++){
            slotsCof[i].SetIndex(i+pageCof);
            slotsCof[i].origen = CofreSlot.Holder.Cofre;
            if(i+pageCof < cofre.savedItems.Count){
                slotsCof[i].SetItem(cofre.savedItems[i+pageCof]);
            }else{
                slotsCof[i].ClearSlot();
            }
        }
    }
}

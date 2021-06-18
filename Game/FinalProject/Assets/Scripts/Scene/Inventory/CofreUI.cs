using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CofreUI : MonoBehaviour
{
    public static CofreUI instance;
    private void Awake() {
        if(instance!=null){
            Debug.Log("mmm esto podria ser malo?");
            return;
        }
        instance = this;
    }
    private Slot focusedSlot;
    public Button nextButtonCof;
    public Button prevButtonCof;
    public Button nextButtonInv;
    public Button prevButtonInv;
    public GameObject menuDesplegable;
    public GameObject cofreUI;
    public Text description;
    public Text nametxt;
    Cofre cofre;
    Inventory inventory;
    public GameObject slotsCofUI;
    public GameObject slotsInvUI;
    CofreSlot[] slotsCof;
    CofreSlot[] slotsInv;

    public Slot moveItemSlot;
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
        menuDesplegable.SetActive(false);
        slotsCof = slotsInvUI.GetComponentsInChildren<CofreSlot>();
        slotsInv = slotsInvUI.GetComponentsInChildren<CofreSlot>();
        
        pageCof = 0;
        pageInv = 0;
        UpdateUI();
        gameObject.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            ForceCloseUI();
        }
    }
    public void NextPageInv(){
        RemoveFocusSlot();
        pageInv += 10;
        if(pageInv > inventory.capacidad + 10){
            pageInv -=10;
            return;
        }
        UpdateUI();
    }
    public void PrevPageInv(){
        RemoveFocusSlot();
        pageInv -= 10;
        if(pageInv < 0){
            pageInv = 0;
            return;
        }
        UpdateUI();
    }
    public void UpdateUI(){
        //Inventory
        for(int i=0; i<slotsInv.Length;i++){
            slotsInv[i].SetIndex(i+pageInv);
            if(i+pageInv < inventory.items.Count){
                slotsInv[i].SetItem(inventory.items.ToArray()[i+pageInv]);
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
            nextButtonInv.gameObject.SetActive(true);
        }
        else{
            nextButtonInv.gameObject.SetActive(false);
        }
        //Cofre
        for(int i=0; i<slotsCof.Length;i++){
            slotsCof[i].SetIndex(i+pageCof);
            if(i+pageCof < cofre.savedItems.Count){
                slotsCof[i].SetItem(cofre.savedItems[i+pageCof]);
            }else{
                slotsCof[i].ClearSlot();
            }
            
        }
        if(pageCof+20 < cofre.savedItems.Count){
            nextButtonCof.gameObject.SetActive(true);
        }
        else{
            nextButtonCof.gameObject.SetActive(false);
        }
        if(pageCof!=0){
            nextButtonCof.gameObject.SetActive(true);
        }
        else{
            nextButtonCof.gameObject.SetActive(false);
        }

        if(focusedSlot!=null){
            focusedSlot.icon.color = Color.yellow;
            focusedSlot.background.color = Color.red;
            if(focusedSlot.GetItem()!=null){
                description.text = focusedSlot.GetItem().descripcion;
                nametxt.text = focusedSlot.GetItem().name;
            }
        }else{
            nametxt.text = "";
            description.text = "Selecciona un objeto";
        }
    }
   
    
    public Slot GetFocusSlot(){
        if(focusedSlot!=null){
            return focusedSlot;
        }
        else{
            return null;
        }
    }
    public void FocusSlot(Slot slot){
        if(focusedSlot!=null){
            focusedSlot.icon.color=Color.white;
            focusedSlot.background.color=Color.white;
        }
        focusedSlot = slot;
        UpdateUI();
    }
    public void RemoveFocusSlot(){
        if(focusedSlot!=null){
            focusedSlot.icon.color=Color.white;
            focusedSlot.background.color=Color.white;
            focusedSlot = null;
        UpdateUI();
        }
    }
    public void ForceCloseUI(){
        cofreUI.SetActive(false);
    }

    public void ShowMenuDesp(){
        menuDesplegable.SetActive(true);
        menuDesplegable.transform.position = Input.mousePosition;
    }
    public void HideMenuDesp(){
        menuDesplegable.SetActive(false);
    }
    public void UseButton(){
        Item item = focusedSlot.GetItem();
        RemoveFocusSlot();
        item.Use();
        HideMenuDesp();
    }
    public void MoveButton(){
        HideMenuDesp();
        if(focusedSlot!=null){
            SetMoveSlot(focusedSlot);
            RemoveFocusSlot();
        }
    }
    public void DeleteButton(){
        Item item = focusedSlot.GetItem();
        RemoveFocusSlot();
        item.RemoveFromInventory();
        HideMenuDesp();
    }
    public void SetMoveSlot(Slot slot){
        moveItemSlot = slot;
    }
    public void RemoveMoveItem(){
        moveItemSlot = null;
    }
    public void MoveItems(Slot destination){
        Item aux = destination.GetItem();
        destination.SetItem(moveItemSlot.GetItem());
        moveItemSlot.SetItem(aux);
        RemoveMoveItem();
    }
   
    public Slot GetMoveItem(){
        return moveItemSlot;
    }
}

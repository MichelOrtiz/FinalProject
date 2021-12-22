using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject cofreUI;
    public Button nextButtonCof;
    public Button prevButtonCof;
    public Button nextButtonInv;
    public Button prevButtonInv;
    public TextMeshProUGUI description;
    public TextMeshProUGUI nametxt;
    private Cofre cofre;
    private Inventory inventory;
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
        if(pageCof+20 < cofre.savedItems.Count){
            nextButtonCof.gameObject.SetActive(true);
        }
        else{
            nextButtonCof.gameObject.SetActive(false);
        }

        if(pageCof!=0){
            prevButtonCof.gameObject.SetActive(true);
        }
        else{
            prevButtonCof.gameObject.SetActive(false);
        }
    }
    public void NextInv(){
        pageInv += 10;
        UpdateUI();
    }
    public void PrevInv(){
        pageInv -= 10;
        UpdateUI();
    }
    public void NextCof(){
        pageCof += 20;
        UpdateUI();
    }
    public void PrevCof(){
        pageCof -= 20;
        UpdateUI();
    }
    public void SetUIActive(bool x){
        
        cofreUI.SetActive(x);
    }
}

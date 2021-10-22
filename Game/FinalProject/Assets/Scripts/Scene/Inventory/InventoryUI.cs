using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    #region ObjectsUI
        public GameObject UI;
        public TextMeshProUGUI nametxt;
        public TextMeshProUGUI description;
        [SerializeField] GameObject menuDesplegable;
        public Transform slotsParent;
        public Button nextButton;
        public Button prevButton;
        [SerializeField] private GameObject InvSlotPrefab;
    #endregion
    
    #region variables
        public InventorySlot focusedSlot {get;set;}
        public static InventoryUI instance;
        Inventory inventory;
        List<InventorySlot> slots;
        [SerializeField] int slotsInPage;
        public ItemSlot moveItem {get;set;}
        private int invPage;
    #endregion
    
    private void Awake() {
        if(instance!=null){
            Debug.Log("mmm esto podria ser malo?");
            return;
        }
        instance = this;
    }
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        invPage = 0;
        menuDesplegable.SetActive(false);
        slots = new List<InventorySlot>();
        for(int i=0; i < slotsInPage; i++){
            GameObject obj = Instantiate(InvSlotPrefab);
            obj.transform.SetParent(slotsParent);
            InventorySlot slot = obj.GetComponent<InventorySlot>();
            slots.Add(slot);
        }
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.inputs.OpenInventory)
        {
            UI.SetActive(!UI.activeSelf);
        }
       
    }
    public void NextPage(){
        invPage += 10;
        if(invPage > inventory.capacidad + 10){
            invPage -=10;
            return;
        }
        UpdateUI();
    }
    public void PrevPage(){
        invPage -= 10;
        if(invPage < 0){
            invPage = 0;
            return;
        }
        UpdateUI();
    }

    public void UpdateUI(){
        UI.SetActive(true);
        for(int i=0; i < slotsInPage && (i+invPage) < inventory.items.Count; i++){
            InventorySlot slot = slots[i];
            Item slotItem = inventory.items[i + invPage];
            slot.SetItem(slotItem);
            slot.SetIndex(i + invPage);
        }

        //Next button
        if(invPage + slotsInPage < inventory.items.Count){
            nextButton.gameObject.SetActive(true);
        }
        else{
            nextButton.gameObject.SetActive(false);
        }

        //Prev button
        if(invPage!=0){
            prevButton.gameObject.SetActive(true);
        }
        else{
            prevButton.gameObject.SetActive(false);
        }
        UpdateText();
        UI.SetActive(false);
    }
    public void UpdateText(){
        nametxt.text = "";
        description.text = "";
        if(focusedSlot == null) return;
        nametxt.text = focusedSlot.GetItem().name;
        description.text = focusedSlot.GetItem().descripcion;
    }
    public void OpenDesMenu(InventorySlot slot){
        menuDesplegable.SetActive(true);
    }
}

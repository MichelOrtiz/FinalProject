using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region Objects
        public static InventoryUI instance;
         public Text nametxt;
        public Text description;
        public Item moveItem;
        public GameObject menuDesplegable;
        public GameObject invetoryUI;
        public Transform itemsParent;
        public Transform itemsParentHotbar0;
        private InventorySlot focusedSlot;
        public Button nextButton;
        public Button prevButton;
        Inventory inventory;
        InventorySlot[] slots;
        InventorySlot[] slotsHotbar0;
    #endregion
    
    #region variables
        public int moveItemIndex;
        private int invPage;
    #endregion
    
    private void Awake() {
        if(instance!=null){
            Debug.Log("HOW!!!");
            return;
        }
        instance = this;
    }
    void Start()
    {
        focusedSlot = null;
        moveItem = null;
        moveItemIndex=0;
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        menuDesplegable.SetActive(false);
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slotsHotbar0 = itemsParentHotbar0.GetComponentsInChildren<InventorySlot>();
        invPage = 0;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            invetoryUI.SetActive(!invetoryUI.activeSelf);
        }
       
    }
    public void NextPage(){
        RemoveFocusSlot();
        invPage += 10;
        if(invPage > inventory.capacidad + 10){
            invPage -=10;
            return;
        }
        UpdateUI();
    }
    public void PrevPage(){
        RemoveFocusSlot();
        invPage -= 10;
        if(invPage < 0){
            invPage = 0;
            return;
        }
        UpdateUI();
    }
    public void UpdateUI(){
        for(int i=0; i<slots.Length;i++){
            slots[i].inventoryIndex = i+invPage;
            if(i+invPage < inventory.items.Count){
                slots[i].SetItem(inventory.items.ToArray()[i+invPage]);
            }else{
                slots[i].ClearSlot();
            }
            
        }
        if(invPage+10 < inventory.items.Count){
            nextButton.gameObject.SetActive(true);
        }
        else{
            nextButton.gameObject.SetActive(false);
        }
        if(invPage!=0){
            prevButton.gameObject.SetActive(true);
        }
        else{
            prevButton.gameObject.SetActive(false);
        }
        if(focusedSlot!=null){
            focusedSlot.icon.color = Color.yellow;
            focusedSlot.background.color = Color.red;
            description.text = focusedSlot.item.descripcion;
            nametxt.text = focusedSlot.item.name;
        }else{
            nametxt.text = "";
            description.text = "Selecciona un objeto";
        }
    }
    public void UpdateHotbar0UI(){
        for(int i=0;i<slotsHotbar0.Length;i++)
        {
            if(inventory.hotbar0[i]!=null)
            {
                slotsHotbar0[i].SetItem(inventory.hotbar0[0]);
                if(!inventory.items.Contains(inventory.hotbar0[0]))
                {
                    inventory.hotbar0[0] = null;
                    slotsHotbar0[i].ClearSlot();
                }
            }
        }
    }
    public void SetItemHotbar(int i)
    {
        if(moveItem!=null)
        {
            inventory.hotbar0[i] = moveItem;
            UpdateHotbar0UI();
        }
        else
        {
            moveItem = null;
        }
    }
    public InventorySlot GetFocusSlot(){
        if(focusedSlot!=null){
            return focusedSlot;
        }
        else{
            return null;
        }
    }
    public void FocusSlot(InventorySlot slot){
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
        invetoryUI.SetActive(false);
    }

    public void ShowMenuDesp(){
        menuDesplegable.SetActive(true);
        menuDesplegable.transform.position = Input.mousePosition;
    }
    public void HideMenuDesp(){
        menuDesplegable.SetActive(false);
    }
    public void UseButton(){
        Item item = focusedSlot.item;
        RemoveFocusSlot();
        item.Use();
        HideMenuDesp();
    }
    public void MoveButton(){
        HideMenuDesp();
        if(focusedSlot!=null){
            SetMoveItem(focusedSlot.item,focusedSlot.inventoryIndex);
            RemoveFocusSlot();
        }
    }
    public void DeleteButton(){
        Item item = focusedSlot.item;
        RemoveFocusSlot();
        item.RemoveFromInventory();
        HideMenuDesp();
    }
    public void SetMoveItem(Item item, int index){
        moveItem = item;
        moveItemIndex = index;
    }
    public void RemoveMoveItem(){
        moveItem = null;
        moveItemIndex = 0;
    }
    public void MoveItems(int destination){
        inventory.SwapItems(moveItemIndex,destination);
        RemoveMoveItem();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public Text nametxt;
    public Text description;
    public InventorySlot focusedSlot;
    private void Awake() {
        if(instance!=null){
            Debug.Log("HOW!!!");
            return;
        }
        instance = this;
    }
    public GameObject invetoryUI;
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;
    private int invPage;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        invPage = 0;
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
        for(int i=0; i<slots.Length;i++){
            if(i+invPage < inventory.items.Count){
                slots[i].SetItem(inventory.items.ToArray()[i+invPage]);
            }else{
                slots[i].ClearSlot();
            }
            
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
    public void FocusSlot(InventorySlot slot){
        if(focusedSlot!=null){
            focusedSlot.icon.color=Color.white;
            focusedSlot.background.color=Color.white;
        }
        focusedSlot = slot;
        UpdateUI();
    }
    public void RemoveFocusSlot(){
        focusedSlot.icon.color=Color.white;
        focusedSlot.background.color=Color.white;
        focusedSlot = null;
        UpdateUI();
    }
    public void ForceCloseUI(){
        invetoryUI.SetActive(false);
    }
}

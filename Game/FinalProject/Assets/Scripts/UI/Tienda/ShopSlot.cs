using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Item item;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button btn_buy;
    [SerializeField] private int price;
    [SerializeField] private int amount;
    [SerializeField] private TextMeshProUGUI txt_amount;
    [SerializeField] private TextMeshProUGUI txt_toChange;
    private void Start() {
        //UpdateUI();
    }
    public void SetItem(Article article){
        item = article.item;
        amount = article.amount;
        price = article.price;
        UpdateUI();
    }
    public void UpdateUI(){
        if(item != null) itemImage.sprite = item.icon;
        if(amount > 0)txt_amount.text = "X" + amount.ToString();
        else txt_amount.text = "";
        if(Inventory.instance.GetMoney() < price){
            btn_buy.GetComponent<Image>().color = Color.red;
        }else{
            btn_buy.GetComponent<Image>().color = Color.green;
        }
    }
    public void Purchase(){
        if(!Inventory.instance.RemoveMoney(price)) return;
        for(int i = 0; i < amount; i++){
            if(!Inventory.instance.Add(item)){
                Debug.Log("No hay espacio");
                for(int x = 0;x < i; x++){
                    Inventory.instance.Remove(item);
                }
                Inventory.instance.AddMoney(price);
            }
        }
        FindObjectOfType<ShopUI>().UpdateUI();
    }
    public void OnPointerEnter(PointerEventData eventData){
        txt_toChange.text = price.ToString()+"G";
    }
    public void OnPointerExit(PointerEventData eventData){
        txt_toChange.text = "Comprar";
    }
}

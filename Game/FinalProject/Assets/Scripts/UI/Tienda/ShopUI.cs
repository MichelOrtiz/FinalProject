using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : InteractionUI
{
    public Article[] articulos;
    public GameObject shopSlot;
    public Transform holder;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        foreach (Article item in articulos)
        {
            GameObject x = Instantiate(shopSlot);
            x.transform.SetParent(holder,false);
            x.GetComponent<ShopSlot>().SetItem(item);
        }
    }
    public void OnClickExit(){
        Exit();
        Destroy(gameObject);
    }
    public void UpdateUI(){
        foreach(ShopSlot slot in holder.GetComponentsInChildren<ShopSlot>()){
            slot.UpdateUI();
        }
    }
}

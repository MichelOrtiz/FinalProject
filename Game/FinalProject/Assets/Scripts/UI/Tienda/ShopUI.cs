using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Article[] articulos;
    public GameObject shopSlot;
    public Transform holder;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Article item in articulos)
        {
            GameObject x = Instantiate(shopSlot);
            x.transform.SetParent(holder,false);
            x.GetComponent<ShopSlot>().SetItem(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

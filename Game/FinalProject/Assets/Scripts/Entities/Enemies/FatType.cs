using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatType : Enemy
{
    [SerializeField] private Item wishedItem;
    [SerializeField] private GameObject wishedItemIcon;

    protected new void Start()
    {
        base.Start();
        //iconSprite = GetComponent<SpriteRenderer>();
        //wishedItemSprite = wishedItem.icon;
        wishedItemIcon.GetComponent<SpriteRenderer>().sprite = wishedItem.icon;
    }

    public override void ConsumeItem(Item item)
    {
        base.ConsumeItem(item);
        if (item == wishedItem)
        {
            Debug.Log("he liked that");
            DestroyEntity();
        }
        else
        {
            Debug.Log("he didn't like that...");
        }
    }
}
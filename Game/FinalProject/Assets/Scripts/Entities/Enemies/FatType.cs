using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatType : Enemy
{
    [SerializeField] private Item wishedItem;
    [SerializeField] private GameObject wishedItemIcon;

    new void Start()
    {
        base.Start();
        //iconSprite = GetComponent<SpriteRenderer>();
        //wishedItemSprite = wishedItem.icon;
        wishedItemIcon.GetComponent<SpriteRenderer>().sprite = wishedItem.icon;
    }

    new void Update()
    {
        base.Update();    
    }


    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        return;
    }

    protected override void Attack()
    {
        return;
    }

    public override void ConsumeItem(Item item)
    {

        if (item == wishedItem)
        {
            Debug.Log("he liked that");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("he didn't like that...");
        }
    }
}
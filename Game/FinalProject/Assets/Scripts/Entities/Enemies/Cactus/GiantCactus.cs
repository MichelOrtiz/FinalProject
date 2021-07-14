using UnityEngine;
using System.Collections.Generic;
using System;
public class GiantCactus : Enemy
{
    [Header("Self Additions")]
    [Header("Items")]
    [SerializeField] private List<Item> commonItems;
    [SerializeField] private Item trash;
    [SerializeField] private Item snitch;


    [Header("Probability")]
    [SerializeField] private float randomItemProbability;
    [SerializeField] private float trashProbability;
    [SerializeField] private float snitchProbability;
    
    public Item RandomItem()
    {
        int random = RandomGenerator.NewRandom(0,commonItems.Count-1);
        return commonItems[random];
    }

    protected override void Attack()
    {
        List<ObjectProbability<Item>> itemProbabilities = new List<ObjectProbability<Item>>
        {
            new ObjectProbability<Item>(trash, trashProbability),
            new ObjectProbability<Item>(snitch, snitchProbability),
            new ObjectProbability<Item>(RandomItem(), randomItemProbability)
        };

        Item itemChosen = RandomGenerator.MatchedElement<Item>(itemProbabilities);

        Inventory.instance.Add(itemChosen);
        base.Attack();
    }

    protected override void MainRoutine()
    {
        enemyMovement.DefaultPatrol();
    }

    protected override void ChasePlayer()
    {
        enemyMovement.DefaultPatrol();
    }
}
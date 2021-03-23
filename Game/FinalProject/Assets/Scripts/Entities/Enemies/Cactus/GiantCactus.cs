using UnityEngine;
using System.Collections.Generic;
public class GiantCactus : Cactus
{
    [Header("Objetos")]
    [SerializeField]
    private Item[] items;
    [SerializeField]
    private Item trash;
    [SerializeField]
    private Item snitch;
    [SerializeField]
    private Dictionary<Item, float> objectProbability;


    
    new void Start()
    {
        base.Start();
        objectProbability = new Dictionary<Item, float>
        {
            {trash, 90.5f},
            {RandomItem(), 9.5f}, 
            {snitch, 0.5f}
        };
    }
    public Item RandomItem(){
        int random = RandomGenerator.NewRandom(0,items.Length-1);
        return items[random];
    }

    new void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
        //Item item = new Item();
        List<float> probabilities = new List<float>(objectProbability.Values);
        int objectChosen = RandomGenerator.MatchedElement(probabilities);
        //Debug.Log(objectChosen);
        Item item = new List<Item>(objectProbability.Keys)[objectChosen];
        //Debug.Log(item.name);
        Inventory.instance.Add(item);
        
    }

    protected override void MainRoutine()
    {
        base.MainRoutine();
    }
}
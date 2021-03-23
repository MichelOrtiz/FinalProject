using UnityEngine;
using System.Collections.Generic;
public class GiantCactus : Cactus
{
    [SerializeField]
    private Dictionary<string, float> objectProbability = new Dictionary<string, float>
    {
        {"Trash", 90.5f},
        {"Berrie", 9.5f}, 
        {"GoldenSnitch", 0.5f}
    };


    
    new void Start()
    {
        base.Start();
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
        Debug.Log(objectChosen);
        Debug.Log(new List<string>(objectProbability.Keys)[objectChosen]);
    }

    protected override void MainRoutine()
    {
        base.MainRoutine();
    }
}
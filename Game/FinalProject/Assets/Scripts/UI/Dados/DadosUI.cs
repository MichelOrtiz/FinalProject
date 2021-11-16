using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosUI : InteractionUI
{
    [SerializeField] private short minNumber;
    [SerializeField] private short maxNumber;

    protected override void Start()
    {
        base.Start();
        RollDice();
    }

    public void RollDice()
    {
        var inventory = Inventory.instance;
        var random =(short) RandomGenerator.NewRandom(minNumber, maxNumber);
        var newMoney = Mathf.Abs(random);
        Debug.Log("random result: " + random);

        if (random < 0)
        {
            inventory.SetMoney(inventory.GetMoney() / newMoney);
        }
        else if (random > 0)
        {
            inventory.SetMoney(inventory.GetMoney() * newMoney);
        }

        Exit();
    }
}

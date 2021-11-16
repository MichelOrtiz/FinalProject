using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DadosUI : InteractionUI
{
    [SerializeField] private short minNumber;
    [SerializeField] private short maxNumber;
    [SerializeField] private TextMeshProUGUI dado;
    protected override void Start()
    {
        base.Start();
        dado.text = "0";
    }

    public void RollDice()
    {
        if(Inventory.instance.GetMoney() <= 0){
            Exit();
        }
        var inventory = Inventory.instance;
        var random =(short) RandomGenerator.NewRandom(minNumber, maxNumber);
        var newMoney = Mathf.Abs(random);
        Debug.Log("random result: " + random);
        dado.text = random.ToString();
        if (random < 0)
        {
            inventory.SetMoney(inventory.GetMoney() / newMoney);
        }
        else if (random > 0)
        {
            inventory.SetMoney(inventory.GetMoney() * newMoney);
        }
    }
    public void Salir(){
        Exit();
    }
}

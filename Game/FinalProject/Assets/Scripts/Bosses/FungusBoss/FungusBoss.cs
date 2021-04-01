using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusBoss : MonoBehaviour
{
    [SerializeField] private float intervalForStaminaLoss;
    [SerializeField] private float staminaLoss;
    [SerializeField] private float numberOfFungus;
    private float timeUntilLoss;
    PlayerManager player;
    public static float defeatedFungus;
    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilLoss > intervalForStaminaLoss)
        {
            player.TakeTirement(staminaLoss);
            timeUntilLoss = 0;
        }
        else
        {
            timeUntilLoss += Time.deltaTime;
        }
        
        if (defeatedFungus == numberOfFungus)
        {
            GetComponent<BossFight>().NextStage();
        }
    }
}

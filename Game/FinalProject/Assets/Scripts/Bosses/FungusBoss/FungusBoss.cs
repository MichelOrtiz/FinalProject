using UnityEngine;

public class FungusBoss : MonoBehaviour
{
    [SerializeField] private float intervalForStaminaLoss;
    [SerializeField] private float staminaLoss;
    [SerializeField] private float numberOfFungus;
    PlayerManager player;
    public float defeatedFungus;
    void Start()
    {
        player = PlayerManager.instance;
        InvokeRepeating("TakeTirement", intervalForStaminaLoss, intervalForStaminaLoss);
    }
    
    void Update()
    {
        if (defeatedFungus == numberOfFungus)
        {
            GetComponent<BossFight>().EndBattle();
            defeatedFungus = 0;
        }
    }

    void TakeTirement()
    {
        player.TakeTirement(staminaLoss);
    }
}

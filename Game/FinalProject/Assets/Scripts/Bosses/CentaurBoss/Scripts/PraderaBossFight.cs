using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraderaBossFight : MonoBehaviour
{
    [SerializeField] private Vector2 raceGoal;
    private PlayerManager player;
    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetPosition().x >= raceGoal.x)
        {
            GetComponent<BossFight>().EndBattle();
        }
    }
}

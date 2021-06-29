using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraderaBossFight : MonoBehaviour
{
    [SerializeField] private Vector2 raceStart;
    [SerializeField] private Vector2 raceGoal;
    private PlayerManager player;
    private BossFight bossFight;
    
    void Awake()
    {
        bossFight = GetComponent<BossFight>();
        bossFight.enabled = false;
    }
    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetPosition().x >= raceStart.x && !bossFight.enabled)
        {
            bossFight.enabled = true;
        }
        if (player.GetPosition().x >= raceGoal.x)
        {
            bossFight.EndBattle();
        }
    }
}

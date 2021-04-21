using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBoss : MonoBehaviour
{
    [SerializeField] private Vector2 finalPos;
    private BossFight bossFight;

    private PlayerManager player;
    void Start()
    {
        bossFight = GetComponent<BossFight>();
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetPosition().x >= finalPos.x)
        {
            Debug.Log("dsd");
            bossFight.EndBattle();
            enabled = false;
        }
    }
}

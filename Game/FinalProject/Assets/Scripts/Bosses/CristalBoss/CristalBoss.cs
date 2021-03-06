using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalBoss : MonoBehaviour
{
    [SerializeField] private GameObject cristalBarPrefab;
    [SerializeField] private GameObject cristalBarWarning;
    
    [SerializeField] private float intervalCristalBars;
    [SerializeField] private float warningTime;
    
    private float currentTimeAfterWarning;
    private float currentTimeBeforeWarning;
    
    private Vector2 cristalBarSpawnPosPlayer;
    private Vector2 cristalBarSpawnPosCristal;

    private GameObject currentWarningPlayer;
    private GameObject currentWarningCristal;

    private PlayerManager player;
    private CristalBossEnemy cristal;

    private RaycastHit2D groundHitForPlayer;
    private RaycastHit2D groundHitForCristal;

    

    void Start()
    {
        player = PlayerManager.instance;
        cristal = FindObjectOfType<CristalBossEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cristal == null)
        {
            cristal = FindObjectOfType<CristalBossEnemy>();
        }
        if (FindObjectOfType<CristalBossEnemy>() == null)
        {
            GetComponent<BossFight>().EndBattle();
            return;
        }

        if (currentTimeBeforeWarning > intervalCristalBars)
        {
            if (currentTimeAfterWarning > warningTime)
            {
                Destroy(currentWarningPlayer);
                Destroy(currentWarningCristal);

                Instantiate(cristalBarPrefab, cristalBarSpawnPosPlayer,  Quaternion.identity);
                Instantiate(cristalBarPrefab, cristalBarSpawnPosCristal,  Quaternion.identity);

                currentTimeAfterWarning = 0;
                currentTimeBeforeWarning = 0;
            }
            else
            {
                if (currentWarningPlayer == null && currentWarningCristal == null)
                {
                    groundHitForPlayer = Physics2D.Raycast(player.GetPosition(), Vector2.down, 30f, 1 << LayerMask.NameToLayer("Ground"));
                    groundHitForCristal = Physics2D.Raycast(cristal.GetPosition(), Vector2.down, 30f, 1 << LayerMask.NameToLayer("Ground"));
                    
                    cristalBarSpawnPosPlayer = groundHitForPlayer.point;
                    cristalBarSpawnPosCristal = groundHitForCristal.point;

                    currentWarningPlayer = Instantiate(cristalBarWarning, cristalBarSpawnPosPlayer, Quaternion.identity);
                    currentWarningCristal = Instantiate(cristalBarWarning, cristalBarSpawnPosCristal, Quaternion.identity);
                }
                else
                {
                    currentTimeAfterWarning += Time.deltaTime;
                }
            }
        }
        else
        {
            currentTimeBeforeWarning += Time.deltaTime;
        }
    }
}
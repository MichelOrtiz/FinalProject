using UnityEngine;
using System.Collections.Generic;
public class RoarDragon : Enemy
{
    //[SerializeField] private float roarAffectDistance;
    [Header("Self Additions")]
    [SerializeField] private float enhanceMultiplier;
    [SerializeField] private float baseEnhancementTime;
    private float enhancementTime;
    private bool roared;
    private bool isRoaring;
    private bool canFallToSurface;
    private List<Enemy> dragons;
    new void Start()
    {
        dragons = ScenesManagers.GetObjectsOfType<Enemy>().FindAll(e => e.enemyType == EnemyType.Dragon);
        base.Start();
    }

    new void Update()
    {
        if (roared)
        {
            if (enhancementTime > baseEnhancementTime)
            {
                foreach (var dragon in dragons)
                {
                    dragon.NerfValues(enhanceMultiplier);
                }
                roared = false;
                enhancementTime = 0;
                ChangeFacingDirection();
            }
            else
            {
                enhancementTime += Time.deltaTime;
            }
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        if (roared)
        {
            enemyMovement.StopMovement();
        }
        base.FixedUpdate();
    }
    protected override void MainRoutine()
    {
        if (!roared)
        {
            enemyMovement.DefaultPatrol();
        }
    }

    protected override void ChasePlayer()
    {
        enemyMovement.StopMovement();
        if (!roared)
        {
            //animator.SetBool("Is Roaring", true);
            animator.SetTrigger("Roar");
            roared = true;
            foreach (var dragon in dragons)
            {
                dragon.EnhanceValues(enhanceMultiplier);
            }
        }
    }
}
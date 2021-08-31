using UnityEngine;
using System.Collections.Generic;
public class RoarDragon : Enemy
{
    //[SerializeField] private float roarAffectDistance;
    [Header("Self Additions")]
    [SerializeField] private float enhanceMultiplier;
    [SerializeField] private float baseEnhancementTime;
    private float enhancementTime;

    [SerializeField] private EnemyStatsModifier statsModifier;
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
            
            /*if (enhancementTime > baseEnhancementTime)
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
            }*/
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        if (roared)
        {
            //enemyMovement.StopMovement();
            return;
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
        //enemyMovement.StopMovement();
        if (!roared)
        {
            //animator.SetBool("Is Roaring", true);
            //animator.SetTrigger("Roar");
            animationManager.ChangeAnimation("roar");
            roared = true;
            EnemyStatsModifier refState = null;
            foreach (var dragon in dragons)
            {
                //dragon.EnhanceValues(enhanceMultiplier);
                var state = dragon.statesManager.AddState(statsModifier);
                if (refState == null)
                {
                    refState = (EnemyStatsModifier) state;
                    refState.StoppedAffect += statsModifier_StoppedAffect;
                }

            }

        }
    }

    void statsModifier_StoppedAffect()
    {
        roared = false;
    }
}
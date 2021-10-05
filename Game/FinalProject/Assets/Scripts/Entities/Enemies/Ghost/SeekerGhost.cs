using UnityEngine;
using System.Collections.Generic;
public class SeekerGhost : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float timeAfterSpawnToStopPush;
    private float curTimeAfterSpawn;
    [SerializeField] private Vector2 pushForceWhenSpawned;


    new void Start()
    {
        base.Start();

        ChangeFacingDirection();
        curTimeAfterSpawn = 0;
    }

    new void FixedUpdate()
    {

        if (curTimeAfterSpawn > timeAfterSpawnToStopPush && rigidbody2d.velocity.magnitude != 0)
        {
            enemyMovement.StopAllMovement();
            //physics.SetKinematic(0.5f);
        }
        else
        {
            curTimeAfterSpawn += Time.deltaTime;
        }
        
        base.FixedUpdate();
    }

    protected override void ChasePlayer()
    {
        enemyMovement.GoTo(player.GetPosition(), chasing: true, gravity: false);
    }

    /*private void Divide()
    {
        if (!alreadyDivided)
        {
            if (ghostsDivisions < maxGhosts)
            {
                SeekerGhost ghost = Instantiate(this, dividePos.position, Quaternion.identity);
                ghost.isOriginal = false;
                ghost.statesManager.RemoveAll();
                clones.Add(ghost);

                ghost.curTimeAfterSpawn = 0;
                ghost.ChangeFacingDirection();
                ghost.Push(facingDirection == RIGHT? -pushForceWhenSpawned.x : pushForceWhenSpawned.x, pushForceWhenSpawned.y);
                alreadyDivided = true;
            }
        }
    }*/
}
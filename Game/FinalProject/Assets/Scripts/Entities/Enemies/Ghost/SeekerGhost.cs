using UnityEngine;
using System.Collections.Generic;
public class SeekerGhost : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private Transform dividePos; 
    [SerializeField] private float baseStartTimeUntilDivide;
    private float startTimeUntilDivide;
    [SerializeField] private float baseMainTimeUntilDivide;
    private float mainTimeUntilDivide;
    [SerializeField] private float timeAfterSpawnToStopPush;
    private float curTimeAfterSpawn;
    [SerializeField] private Vector2 pushForceWhenSpawned;
    [SerializeField] private byte maxGhosts;
    private bool alreadyDivided;
    private byte ghostsDivisions;


    [SerializeReference] private List<SeekerGhost> clones;

    new void Awake()
    {
        base.Awake();
        clones = new List<SeekerGhost>();

    }

    new void Start()
    {
        base.Start();
        curTimeAfterSpawn = 0;
    }

    new void FixedUpdate()
    {
        ghostsDivisions = (byte)clones.Count;//ScenesManagers.GetObjectsOfType<SeekerGhost>().Count;


        if (ghostsDivisions == 0)
        {
            if (startTimeUntilDivide < baseStartTimeUntilDivide)
            {
                startTimeUntilDivide += Time.deltaTime;
            }
            else
            {
                Divide();
            }
        }
        else
        {
            if (mainTimeUntilDivide < baseMainTimeUntilDivide)
            {
                mainTimeUntilDivide += Time.deltaTime;
            }
            else
            {
                Divide();
                mainTimeUntilDivide = 0;
            }
        }

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

    private void Divide()
    {
        if (!alreadyDivided)
        {
            if (ghostsDivisions < maxGhosts)
            {
                SeekerGhost ghost = Instantiate(this, dividePos.position, Quaternion.identity);
                ghost.statesManager.RemoveAll();
                clones.Add(ghost);

                ghost.curTimeAfterSpawn = 0;
                ghost.ChangeFacingDirection();
                ghost.Push(facingDirection == RIGHT? -pushForceWhenSpawned.x : pushForceWhenSpawned.x, pushForceWhenSpawned.y);
                alreadyDivided = true;
            }
        }
    }
}
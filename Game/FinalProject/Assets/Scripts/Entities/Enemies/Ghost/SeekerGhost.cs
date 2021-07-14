using UnityEngine;
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
    [SerializeField] private int maxGhosts;
    private bool alreadyDivided;
    private int ghostsDivisions;


    new void Start()
    {
        base.Start();
        curTimeAfterSpawn = 0;
    }

    new void FixedUpdate()
    {
        ghostsDivisions = ScenesManagers.GetObjectsOfType<SeekerGhost>().Count;
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
            enemyMovement.StopMovement();
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
                ghost.curTimeAfterSpawn = 0;
                ghost.ChangeFacingDirection();
                ghost.Push(facingDirection == RIGHT? -pushForceWhenSpawned.x : pushForceWhenSpawned.x, pushForceWhenSpawned.y);
                alreadyDivided = true;
            }
        }
    }
}
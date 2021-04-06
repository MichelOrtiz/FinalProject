using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalBossEnemy : NormalType, ILaser
{
    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    [SerializeField] private float minDistanceToShotRay;
    [SerializeField] private float intervalToShot;
    private float timeToShot;

    //[SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;
    [SerializeField] private float laserDamage;
    [SerializeField] private float laserSpeed;

    #endregion

    #region RunFromPlayerParams
    [Header("Run from player")]
    [SerializeField] private float minDistanceToPlayer;
    private float distanceToPlayer;

    [SerializeField] private List<Transform> platformPositions;
    private Transform nearestPlatform;
    private bool runningFromPlayer;
    #endregion

    #region Player Interaction
    [Header("Player Interaction")]
    [SerializeField] private float interactionRadius;
    [SerializeField] private int interactionsToDestroy;
    private int interactions;

    #endregion

    new void Start()
    {
        base.Start();
        laserSpeed *= averageSpeed;
    }

    new void Update()
    {
        if (!runningFromPlayer)
        {
            if (InFrontOfObstacle() ||( (GetPosition().x > player.GetPosition().x && facingDirection == RIGHT)
            || GetPosition().x < player.GetPosition().x && facingDirection == LEFT) )
            {
                ChangeFacingDirection();
            }
        }
        else
        {
            if (InFrontOfObstacle() ||( (GetPosition().x > player.GetPosition().x && facingDirection == LEFT)
            || GetPosition().x < player.GetPosition().x && facingDirection == RIGHT ))
            {
                ChangeFacingDirection();
            }
        }
        
        distanceToPlayer = Vector2.Distance(GetPosition(), player.GetPosition());

        if (distanceToPlayer <= interactionRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactions++;
            }
        }
        

        if (interactions == interactionsToDestroy)
        {
            Destroy(gameObject);
        }

        base.Update();
    }


    public override void ConsumeItem(Item item)
    {
        return;
    }

    protected override void Attack()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        if (distanceToPlayer >= minDistanceToShotRay)
        {
            if (timeToShot > intervalToShot)
            {
                if (laser == null)
                {
                    ShootLaser(shotPos.position, player.GetPosition());
                }
                timeToShot = 0;
            }
            else
            {
                timeToShot += Time.deltaTime;
            }
            
        }
        else
        {
            timeToShot = 0;
        }

            if (distanceToPlayer <= minDistanceToPlayer)
            {
                if (nearestPlatform == null)
                {
                    nearestPlatform = FindNearestPlatform();
                    runningFromPlayer = true;
                }


            }
            if (runningFromPlayer)
            {
                rigidbody2d.position = Vector2.MoveTowards(GetPosition(), nearestPlatform.position, chaseSpeed * Time.deltaTime);

                runningFromPlayer = rigidbody2d.position != (Vector2)nearestPlatform.position;
            }        
            else
            {
                nearestPlatform = null;
            }
    }

    protected override void MainRoutine(){}

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, laserSpeed, this);
    }
    public void LaserAttack()
    {
        player.TakeTirement(laserDamage);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rigidbody2d.AddForce(new Vector2((facingDirection == RIGHT? 5000f : -5000f), jumpForce * 450), ForceMode2D.Impulse);
        }
    }

    private bool OnPlatform()
    {
        return Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround).gameObject.CompareTag("Platform");
    }
    

    private Transform FindNearestPlatform()
    {
        Transform pos;
        
        float curDistance;
        List<Transform> platforms;
        if (facingDirection == LEFT)
        {
            platforms = platformPositions.FindAll(p => p.position.x > GetPosition().x);
        }
        else
        {
            platforms = platformPositions.FindAll(p => p.position.x < GetPosition().x);
        }
        
        float shortestDistance = 10f;//Vector2.Distance(GetPosition(), platformPositions[0].position);
        
        if (platforms.Count == 0)
        {
            platforms = platformPositions;
        }

        pos = platforms[0];


        foreach (var platformPos in platforms)
        {   
            curDistance = Vector2.Distance(GetPosition(), platformPos.position);
            if (curDistance < shortestDistance && curDistance > 2f && !( 
                    (platformPos.position.x < GetPosition().x && player.GetPosition().x < GetPosition().x) &&
                    (platformPos.position.x > GetPosition().x && player.GetPosition().x > GetPosition().x) ) )
            {
                shortestDistance = curDistance;
                pos = platformPos;
            }
        }
        return pos;
    }
}

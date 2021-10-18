using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SeekerCentaurBoss : Entity
{
    [Header("Effect On Player")]
    [SerializeField] private State enemyEffectOnPlayer;
    [SerializeField] private State enemyEffectOnSelf;
    [SerializeField] private float damageAmount;


    [Header("Projectile")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private float minDistanceToShoot;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;


    #region PathFinding variables
    [Header("Pathfinfing")]
    [SerializeField] private Transform target;
    [SerializeField] private float activateDistance;
    [SerializeField] private float pathUpdateSeconds;

    [Header("Physics")] 
    [SerializeField] private float speedMultiplier;
    private float speed;
    [SerializeField] private float nextWaypointDistance;
    [SerializeField] private float jumpNodeHeightRequirement;

    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    #endregion

    [Header("Speed Increase")]
    [SerializeField] private float minDistanceFromPlayer;
    [SerializeField] private float speedIcreaseMultiplier;
    private  bool speedIncreased;

    [Header("Misc")]
    [SerializeField] private Transform fovOrigin;
    [SerializeField] private float baseCastDistance;
    [SerializeField] private bool canMove = true;

    [SerializeField] private float waitTime;
    private float currentTime;

    private PlayerManager player;
    private bool touchingPlayer;

    void Awake()
    {
        collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;
        collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;

    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        statesManager = GetComponent<StatesManager>();
        player = PlayerManager.instance;
        target = player.transform;
        speed = averageSpeed * speedMultiplier;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);  
    }

    // Update is called once per frame
    new void Update()
    {
        float distanceFromPlayer = Vector2.Distance(GetPosition(), player.GetPosition());
        if (distanceFromPlayer >= minDistanceToShoot)
        {
            if (currentTimeBtwShot > timeBtwShot)
            {
                projectileShooter.ShootProjectile(player.GetPosition());
                currentTimeBtwShot = 0;
            }
            else
            {
                currentTimeBtwShot += Time.deltaTime;
            }
        }
        else
        {
            currentTimeBtwShot = 0;
        }

        if (distanceFromPlayer <= minDistanceFromPlayer)
        {
            if (!speedIncreased)
            {
                speed *= speedIcreaseMultiplier;
                speedIncreased = true;
            }
            
        }
        else if (speedIncreased)
        {
            speed /= speedIcreaseMultiplier;
            speedIncreased = false;
        }

        base.Update();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (TargetInDistance() && canMove)
        {
            FollowPath();
        }
    }

    void UpdatePath()
    {
        if (TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(GetPosition(), target.position, OnPathComplete);
        }
    }

    void FollowPath()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - (Vector2) GetPosition()).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rigidbody2d.AddForce(Vector2.up * speed * jumpForce, ForceMode2D.Impulse);
            }

        }
        else
        {
            force.y = 0;
        }
        
        rigidbody2d.AddForce(force, ForceMode2D.Impulse);

        // Movement

        // Next Waypoint
        float distance = Vector2.Distance(GetPosition(), path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

         /*if (rigidbody2d.velocity.x> speed)
         {
             rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
         }
         else if (rigidbody2d.velocity.x <speed*(-1))
         {
             rigidbody2d.velocity = new Vector2(speed*(-1), rigidbody2d.velocity.y);
         }*/

        /* if (rigidbody2d.velocity.x > 0.05f && facingDirection == LEFT)
         {
            transform.eulerAngles = new Vector3(0, 0); 
         }
         else if (rigidbody2d.velocity.x < -0.05f && facingDirection == RIGHT)
         {
            transform.eulerAngles = new Vector3(0, 180); 
         }*/
         if (( facingDirection == RIGHT && player.GetPosition().x < GetPosition().x )
                || ( facingDirection == LEFT && player.GetPosition().x > GetPosition().x ))
            {
                transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180); 
            }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(GetPosition(), target.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    protected bool InFrontOfObstacle()
    {

        float castDistance = facingDirection == LEFT ? -baseCastDistance : baseCastDistance;
        Vector3 targetPos = fovOrigin.position + (facingDirection == LEFT? Vector3.left : Vector3.right) * castDistance;
        return RayHitObstacle(fovOrigin.position, targetPos);
    }

    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            player.TakeTirement(damageAmount);
            player.statesManager.AddState(enemyEffectOnPlayer);
            statesManager.AddState(enemyEffectOnSelf);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            touchingPlayer = false;
        }        
    }*/

    new void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.gameObject.tag == "Player")
        {
            Debug.Log("touching player");
            touchingPlayer = true;
            player.TakeTirement(damageAmount);
            player.statesManager.AddState(enemyEffectOnPlayer);
            statesManager.AddState(enemyEffectOnSelf);
        }
    }

    new void collisionHandler_ExitContact(GameObject contact)
    {
        if (contact.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
}

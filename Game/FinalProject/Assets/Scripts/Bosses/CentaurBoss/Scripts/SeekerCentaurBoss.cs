using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SeekerCentaurBoss : Entity, IProjectile
{
    [Header("Effect On Player")]
    [SerializeField] private State effectOnPlayer;
    
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
    private PlayerManager player;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        player = PlayerManager.instance;
        target = player.transform;
        speed = averageSpeed * speedMultiplier;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);  
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (TargetInDistance())
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

        if (!isGrounded) force.y = 0;
        
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

         if (rigidbody2d.velocity.x > 0.05f && facingDirection == LEFT)
         {
            transform.eulerAngles = new Vector3(0, 0); 
         }
         else if (rigidbody2d.velocity.x < -0.05f && facingDirection == RIGHT)
         {
            transform.eulerAngles = new Vector3(0, 180); 
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

    public void ProjectileAttack()
    {
        //throw new System.NotImplementedException();
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        //throw new System.NotImplementedException();
    }
}

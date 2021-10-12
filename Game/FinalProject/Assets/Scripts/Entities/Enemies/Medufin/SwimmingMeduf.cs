using UnityEngine;
using System;
using Pathfinding;
public class SwimmingMeduf : Enemy
{
    [Header("Self Additions")]

    #region PathFinding variables
    [Header("Pathfinfing")]
    [SerializeField] private Transform target;
    [SerializeField] private float pathUpdateSeconds;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float nextWaypointDistance;
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    
    #endregion

    new void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        target = player.transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    new void Update()
    {
        // Enables or disables the Surface graph to be transvarsable, so it can follow a path there
        /*if (Vector2.Distance(fieldOfView.FovOrigin.position, player.GetPosition()) <= secondFovDistance)
        {
            if (!surfaceActive)
            {
                seeker.traversableTags = MathUtils.EditBitInBitmask(seeker.traversableTags, surfaceTagIndex, true);
                surfaceActive = true;
                lastGroundPos = GetPosition();
            }
        }
        else
        {
            if (surfaceActive)
            {
                seeker.traversableTags = MathUtils.EditBitInBitmask(seeker.traversableTags, surfaceTagIndex, false);
                surfaceActive = false;
            }
        }*/
        base.Update();
    }

    protected override void MainRoutine()
    {
        enemyMovement.DefaultPatrolInAir();
    }

    protected override void ChasePlayer()
    {
        FollowPath();
    }

    #region Pathfinding Stuff
    void UpdatePath()
    {
        if ( seeker.IsDone())
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
        Vector2 force = direction * enemyMovement.ChaseSpeed * Time.deltaTime;

        // enemyMovement.Translate(direction, chasing: surfaceActive);
        rigidbody2d.AddForce(force, ForceMode2D.Force);
        // Next Waypoint
        float distance = Vector2.Distance(GetPosition(), path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (( facingDirection == RIGHT && player.GetPosition().x < GetPosition().x )
                || ( facingDirection == LEFT && player.GetPosition().x > GetPosition().x ))
            {
                transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180); 
            }
    }


    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    #endregion
}
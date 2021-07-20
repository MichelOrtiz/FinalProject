using UnityEngine;
using System;
using Pathfinding;
public class MoleCactus : Enemy
{
    [Header("Self Additions")]
    [Header("Surface")]
    [SerializeField] private float secondFovDistance;
    [SerializeField] private byte surfaceTagIndex;
    private bool surfaceActive;

    #region PathFinding variables
    [Header("Pathfinfing")]
    [SerializeField] private Transform target;
    [SerializeField] private float activateDistance;
    [SerializeField] private float pathUpdateSeconds;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float nextWaypointDistance;
    [SerializeField] private float jumpNodeHeightRequirement;
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;
    private Vector2 lastGroundPos;
    
    #endregion

    new void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        target = player.transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        lastGroundPos = GetPosition();
    }

    new void Update()
    {
        // Enables or disables the Surface graph to be transvarsable, so it can follow a path there
        if (Vector2.Distance(fieldOfView.FovOrigin.position, player.GetPosition()) <= secondFovDistance)
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
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        enemyMovement.GoTo(lastGroundPos, chasing: false, gravity: false);
    }

    protected override void ChasePlayer()
    {
        if (TargetInDistance() && canMove)
        {
            FollowPath();
        }
    }

    #region Pathfinding Stuff
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
        Vector2 force = direction * (surfaceActive? enemyMovement.ChaseSpeed : enemyMovement.DefaultSpeed) * Time.deltaTime;

        // Movement
        enemyMovement.GoTo((Vector2) path.vectorPath[currentWaypoint], chasing: surfaceActive, gravity: false);
        //enemyMovement.Translate(direction, chasing: surfaceActive);
        //rigidbody2d.AddForce(force, ForceMode2D.Force);
        // Next Waypoint
        float distance = Vector2.Distance(GetPosition(), path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
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
    #endregion
}
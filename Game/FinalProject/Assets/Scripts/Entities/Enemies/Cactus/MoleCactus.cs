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

    [SerializeField] private Vector2 groundCheckerToSurface;
    [SerializeField] private Vector2 groundCheckerToGround;

    bool justStarted = true;
    bool inGround;


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

    protected override void groundChecker_ExitGround()
    {
        if(!canMove) return;
        justStarted = false;
        inGround = false;
        Debug.Log("ExitGround");
        canMove = false;

        var groundCheckerPos = groundChecker.transform.position;

        transform.position = new Vector2(transform.position.x, groundCheckerPos.y + +2f);
        animationManager.ChangeAnimation("exit_ground");
        animationManager.SetNextAnimation("exit_ground_2");
        Invoke("ActivateMove", 4f);

        //groundChecker.transform.localPosition = groundCheckerToGround;
    }

    protected override void groundChecker_Grounded(string groundTag)
    {
        if (justStarted || !canMove) return;
        inGround = true;
        Debug.Log("EnterGround");
        canMove = false;

        var groundCheckerPos = groundChecker.transform.position;

        transform.position = new Vector2(transform.position.x, groundCheckerPos.y + -2f);
        animationManager.ChangeAnimation("enter_ground");
        animationManager.SetNextAnimation("enter_ground_2");
        Invoke("ActivateMove", 4f);

        //groundChecker.transform.localPosition = groundCheckerToSurface;
    }

    void ActivateMove()
    {
        canMove = true;
        animationManager.nextStateEnabled = false;
    }

    new void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        target = player.transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        lastGroundPos = GetPosition();

        //groundChecker.transform.localPosition = groundCheckerToSurface;

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
        animationManager.ChangeAnimation("idle_in_ground");
        enemyMovement.GoTo(lastGroundPos, chasing: false, gravity: false);
    }

    protected override void ChasePlayer()
    {
        if (TargetInDistance() && canMove)
        {
            
            //animationManager.ChangeAnimation("idle_in_ground", enemyMovement.ChaseSpeed / 4);
            FollowPath();
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        try
        {
            if (canMove)
            {
                groundChecker.transform.position = Vector3.MoveTowards(groundChecker.transform.position, path.vectorPath[currentWaypoint], enemyMovement.ChaseSpeed * Time.deltaTime );
            }
             
        }
        catch (System.Exception)
        {
            
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
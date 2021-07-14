using UnityEngine;

public class HangingArandana : Enemy
{
    
    [Header("Self Additions")]
    [SerializeField] float waitTimeWhenReach;
    private float curWaitTime;
    [SerializeField] float maxViewDistance;
    [SerializeField] float maxThreadDistance;
    [SerializeField] LineRenderer thread;
    [SerializeField] Transform threadPosition;
    private bool justChasedPlayer;
    private bool goingBack;
    private bool waiting;
    private Vector3 startPosition;
    private Vector3 lastSeenPlayerPosition;

    #region Unity stuff
    new void Start()
    {
        base.Start();
        curWaitTime = waitTimeWhenReach;
        startPosition = this.GetPosition();
    }

    new void Update()
    {
        isChasing = fieldOfView.canSeePlayer || justChasedPlayer;
        thread.SetPosition(0, threadPosition.position);
        RaycastHit2D hit = FieldOfView.GetRaycastOnColliderHit(threadPosition.position, Vector2.up, maxThreadDistance, fieldOfView.WhatIsObstacle);
        thread.SetPosition(1, hit.point);

        fieldOfView.SetViewDistanceOnRayHitObstacle(Vector2.down, maxViewDistance);

        if (touchingPlayer)
        {
            lastSeenPlayerPosition = GetPosition();
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        if (justChasedPlayer)
        {
            if (this.GetPosition() != lastSeenPlayerPosition)
            {
                GoToPlayer();
            }

            if (curWaitTime > 0)
            {
                waiting = true;
                curWaitTime -= Time.deltaTime;
                return;
            }
            curWaitTime = waitTimeWhenReach;
            waiting = false;
            goingBack = true;
            justChasedPlayer = false;
        }
        if (goingBack)
        {
            if (this.GetPosition() != startPosition)
            {
                enemyMovement.GoTo(startPosition, chasing: false, gravity: false);
                return;
            }
            goingBack = false;
        }
        base.FixedUpdate();
    }

    void LateUpdate()
    {
        justChasedPlayer = justChasedPlayer && !fieldOfView.canSeePlayer;
    }
    #endregion

    #region Behaviour methods
    protected override void ChasePlayer()
    {
        if (!waiting)
        {
            lastSeenPlayerPosition = new Vector2(this.GetPosition().x, player.GetPosition().y);
        }
        GoToPlayer();
        justChasedPlayer = true;  
    }

    protected override void MainRoutine()
    {
        return;
    }

    void GoToPlayer()
    {
        if (Vector2.Distance(GetPosition(), startPosition) < maxThreadDistance -1f && !touchingPlayer)
        {
            enemyMovement.GoTo(lastSeenPlayerPosition, chasing: true, gravity: false);
        }
    }
    #endregion
}
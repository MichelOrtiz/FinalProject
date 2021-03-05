using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingArandana : Arandaña
{
    private bool justChasedPlayer;
    private bool goingBack;
    private bool waiting;
    private Vector3 startPosition;
    private Vector3 lastSeenPlayerPosition;
    private float timer = 5;

    [SerializeField] float maxViewDistance;

    #region Unity stuff
    new void Start()
    {
        base.Start();
        startPosition = this.GetPosition();

        RaycastHit2D hit = Physics2D.Linecast(fovOrigin.position, fovOrigin.position + Vector3.down * maxViewDistance, 1 << LayerMask.NameToLayer("Ground"));
        if (hit.collider == null)
        {
            viewDistance = maxViewDistance;
        }
        else 
        {
            viewDistance = hit.distance;
        }
        //viewDistance = 
    }

    new void Update()
    {
        isChasing = CanSeePlayer() || justChasedPlayer;
        base.Update();
    }

    new void FixedUpdate()
    {
        if (justChasedPlayer)
        {
            if (this.GetPosition() != lastSeenPlayerPosition)
            {
                rigidbody2d.position = Vector3.MoveTowards(GetPosition(), lastSeenPlayerPosition, chaseSpeed * Time.deltaTime);
            }

            if (timer > 0)
            {
                waiting = true;
                timer -= Time.deltaTime;
                return;
            }
            timer = 5f;
            waiting = false;
            goingBack = true;
            justChasedPlayer = false;
        }
        if (goingBack)
        {
            if (this.GetPosition() != startPosition)
            {
                rigidbody2d.position = Vector3.MoveTowards(GetPosition(), startPosition, normalSpeed * Time.deltaTime);
                return;
            }
            goingBack = false;
        }
        base.FixedUpdate();
    }

    void LateUpdate()
    {
        justChasedPlayer = justChasedPlayer && !CanSeePlayer();
    }
    #endregion

    #region Behaviour methods
    protected override void ChasePlayer()
    {
        if (!waiting)
        {
            lastSeenPlayerPosition = new Vector3(this.GetPosition().x, player.GetPosition().y);//new Vector3(, player.GetPosition().y);
        }
        if (!touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), lastSeenPlayerPosition, chaseSpeed * Time.deltaTime);
        }
        justChasedPlayer = true;
        
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }
    #endregion
}
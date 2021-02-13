using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotterGnome : Gnome
{
    // To check if the player was being chased
    private bool justChasedPlayer;
    private Vector3 lastSeenPlayerPosition;
    float timer = 3;

    protected new void Start()
    {
        base.Start();
        waitTime = 2; // to wait 2 seconds before changing facing direction
        
    }

    protected new void Update()
    {
        isChasing = CanSeePlayer() || justChasedPlayer;
        base.Update();
    }

    protected new void FixedUpdate()
    {
        if (justChasedPlayer)
        {
            if (this.GetPosition().x != lastSeenPlayerPosition.x)
            {
                if (!InFrontOfObstacle() && isGrounded)
                {
                    rigidbody2d.position = Vector3.MoveTowards(this.GetPosition(), new Vector3(lastSeenPlayerPosition.x, 0), chaseSpeed * rigidbody2d.gravityScale * Time.deltaTime);
                }
                else
                {
                    lastSeenPlayerPosition = this.GetPosition();
                }
            }
            else
            {
                isWalking = false;
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    return;
                }
                justChasedPlayer = false;
                timer = 2f;
            }
        }
        base.FixedUpdate();
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        // if ChasePlayer() was just called in update, checks if can not longer see player to update the boolean
        justChasedPlayer = justChasedPlayer && !CanSeePlayer();
    }
    protected override void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(this.GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
        }
        lastSeenPlayerPosition = player.GetPosition();

        justChasedPlayer = true;
    }

    protected override void Attack()
    {
        player.Captured(nTaps: 10, damageAmount);
    }
}

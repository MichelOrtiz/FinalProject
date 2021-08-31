using UnityEngine;
public class Arandana : Enemy
{
    new void FixedUpdate()
    {
        if (groundChecker.isNearEdge && !groundChecker.isGrounded)
        {
            enemyMovement.StopMovement();
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90 );
        }
        
        if (fieldOfView.inFrontOfObstacle)
        {
            enemyMovement.StopMovement();
            ChangeFacingDirection();
        }

        if (touchingPlayer)
        {
            enemyMovement.StopAllMovement();
        }
        base.FixedUpdate();
    }

    protected override void MainRoutine()
    {
        if (groundChecker.isGrounded && !fieldOfView.inFrontOfObstacle && !touchingPlayer)
        {
            enemyMovement.Translate(transform.right, chasing: false);
            animationManager.ChangeAnimation("walk");
        }
    }

    protected override void ChasePlayer()
    {
        if (groundChecker.isGrounded && !fieldOfView.inFrontOfObstacle && !touchingPlayer)
        {
            // player has to be in the same ground to use chasing speed
            enemyMovement.Translate(transform.right, chasing: player.groundChecker.lastGroundTag == groundChecker.lastGroundTag);
            animationManager.ChangeAnimation("walk", enemyMovement.ChaseSpeed * 1 / enemyMovement.DefaultSpeed);
        }
    }
}
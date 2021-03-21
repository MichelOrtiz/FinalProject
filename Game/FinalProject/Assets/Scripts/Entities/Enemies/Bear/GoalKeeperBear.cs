using UnityEngine;
public class GoalKeeperBear : Bear
{
    private float initialChaseSpeed;
    new void Start()
    {
        base.Start();
        initialChaseSpeed = chaseSpeed;
    }
    new void Update()
    {
        if (InFrontOfObstacle())
        {
            //rigidbody2d.position = Vector3.MoveTowards(GetPosition(), new Vector3(GetPosition().x, jumpForce), chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
            //rigidbody2d.AddForce(Vector3.up * jumpForce * Time.deltaTime * rigidbody2d.gravityScale, ForceMode2D.Force);
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x * normalSpeed * Time.deltaTime, jumpForce * rigidbody2d.gravityScale);

        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        chaseSpeed = initialChaseSpeed;
        base.MainRoutine();
    }

    protected override void ChasePlayer()
    {
        if (facingDirection == LEFT && player.GetPosition().x > this.GetPosition().x || facingDirection == RIGHT && player.GetPosition().x < this.GetPosition().x)
        {
            ChangeFacingDirection();
        }
        chaseSpeed += 1/Mathf.Abs(player.GetPosition().x - this.GetPosition().x);
        if (!touchingPlayer && isGrounded)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);

            /*if (InFrontOfObstacle())
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x * chaseSpeed * Time.deltaTime, jumpForce * rigidbody2d.gravityScale);
            }*/
        }
        else
        {
            chaseSpeed = initialChaseSpeed;
        }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }
}
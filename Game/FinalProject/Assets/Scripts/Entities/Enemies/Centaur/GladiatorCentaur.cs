using UnityEngine;
public class GladiatorCentaur : Centaur
{
    [SerializeField] protected bool touchingEnemy;
    [SerializeField] private float baseWaitTimeAfterPush;
    [SerializeField] private float pushEnemyForceX;
    [SerializeField] private float pushEnemySpeedX;
    [SerializeField] private float pushEnemyForceY;
    [SerializeField] private float pushEnemySpeedY;
    [SerializeField] private float pushPlayerForceX;
    [SerializeField] private float pushPlayerSpeedX;
    [SerializeField] private float pushPlayerForceY;
    [SerializeField] private float pushPlayerSpeedY;
    private float waitTimeAfterPush;
    private bool pushedPlayer;
    private Enemy enemyTouched;
    new void Start()
    {
        base.Start();
    }
    
    new void Update()
    {
        if (pushedPlayer)
        {
            if (waitTimeAfterPush < baseWaitTimeAfterPush)
            {
                waitTimeAfterPush += Time.deltaTime;
            }
            else
            {
                pushedPlayer = false;
                waitTimeAfterPush = 0;
            }
        }

        if (eCollisionHandler.touchingEnemy)
        {
            enemyTouched = eCollisionHandler.lastEnemyTouched;
        }
        base.Update();
    }

    protected override void ChasePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        Vector3 playerPosition = (player.isGrounded? player.GetPosition(): new Vector3(player.GetPosition().x, GetPosition().y));
        if (!pushedPlayer && isGrounded && !touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, chaseSpeed * Time.deltaTime);
        }
    }

    protected override void MainRoutine()
    {
        if (!pushedPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemies");
            base.MainRoutine();
        }
    }

    new void FixedUpdate()
    {
        if (touchingPlayer && !pushedPlayer)
        {
            // has to be called here instead of Attack() since is physics related
            //player.Push((facingDirection == RIGHT? pushForceX : -pushForceX) * pushSpeedX, 0f);
            player.Push((facingDirection == RIGHT? pushPlayerForceX : -pushPlayerForceX) * pushPlayerSpeedX, pushPlayerForceY * pushPlayerSpeedY);
            pushedPlayer = true;
        }
        if (touchingEnemy)
        {
            enemyTouched.Push((facingDirection == RIGHT? pushEnemyForceX : -pushEnemyForceX) * pushEnemySpeedX, pushEnemyForceY * pushEnemySpeedY);
        }
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
        //player.Push(pushForce, pushSpeed);
    }

    /*protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            enemyTouched = other.gameObject.GetComponent<Enemy>();
        }
    }

    protected override void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);
        if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
        }
    }*/

}
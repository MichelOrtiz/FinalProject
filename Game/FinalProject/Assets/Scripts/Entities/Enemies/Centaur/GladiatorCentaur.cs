using UnityEngine;
public class GladiatorCentaur : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float waitTimeAfterPush;
    private float curTimeAfterPush;
    

    [SerializeField] private float enemyPushAngle;
    [SerializeField] private float enemyPushForce;
    [SerializeField] private float enemyPushTime;

    
    private bool pushedPlayer;
    private Enemy enemyTouched;

    new void Start()
    {
        base.Start();
        eCollisionHandler.TouchedEnemyHandler += eCollisionHandler_TouchedEnemy;
    }
    
    new void Update()
    {
        if (pushedPlayer)
        {
            if (curTimeAfterPush > waitTimeAfterPush)
            {
                pushedPlayer = false;
                curTimeAfterPush = 0;
            }
            else
            {
                curTimeAfterPush += Time.deltaTime;
            }
        }
        base.Update();
    }

    protected override void ChasePlayer()
    {
        if (!pushedPlayer)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: true);
        }
    }

    protected override void MainRoutine()
    {
        if (!touchingPlayer && !pushedPlayer)
        {
            enemyMovement.DefaultPatrol();
        }
    }

    new void FixedUpdate()
    {
        //if (touchingPlayer && !pushedPlayer)
        {
            // has to be called here instead of Attack() since is physics related
            //player.Push((facingDirection == RIGHT? pushForceX : -pushForceX) * pushSpeedX, 0f);
            //player.Push(facingDirection == RIGHT? playerPushForce.x : -playerPushForce.x, playerPushForce.y);
           // player.rigidbody2d.AddForce(new Vector2(facingDirection == RIGHT? playerPushForce.x : -playerPushForce.x, playerPushForce.y));
            //pushedPlayer = true;
        }
        /*if (eCollisionHandler.touchingEnemy)
        {
            enemyTouched = eCollisionHandler.lastEnemyTouched;
            enemyTouched.Push(facingDirection == RIGHT? enemyPushForce.x : -enemyPushForce.x, enemyPushForce.y);
        }*/
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        base.Attack();
        enemyMovement.StopMovement();
    }
    
    protected override void KnockbackEntity(Entity entity)
    {
        if (rigidbody2d.velocity.magnitude == 0) return;

        if (entity == player)
        {
            base.KnockbackEntity(player);
            pushedPlayer = true;
        }
        else
        {
            entity.Knockback
            (
                enemyPushTime, 
                enemyPushForce,
                
                facingDirection == entity.facingDirection ?
                entity.transform.InverseTransformPoint
                (
                    entity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(enemyPushAngle)
                    ))
                    :
                -entity.transform.InverseTransformPoint
                (
                    entity.GetPosition() + 
                    (MathUtils.GetVectorFromAngle(-enemyPushAngle)
                    ))
            );
        }
    }

    void eCollisionHandler_TouchedEnemy(Enemy enemy)
    {
        if (isChasing)
        {
            KnockbackEntity(enemy);
        }
    }

}
using UnityEngine;
public class HockeyPlayer : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float maxViewDistance;
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;

    [SerializeField] private float pushTime;
    [SerializeField] private Vector2 pushVector;
    [SerializeField] private float pushForce;
    public bool IsOnIce { get => collisionHandler.Contacts.Exists(g => g.tag == "Ice"); }

    new void Start()
    {
        base.Start();
        /*if (pushForceMultiplier.magnitude > 0f)
        {
            pushForce *= pushForceMultiplier;
        }*/
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
    }
    
    new void Update()
    {
        fieldOfView.SetViewDistanceOnRayHitObstacle(facingDirection == RIGHT ? Vector2.right : Vector2.left, maxViewDistance);
        //isOnIce = groundChecker.lastGroundTag == "Ice";
        
        base.Update();
    }

    protected override void MainRoutine()
    {
        if (IsOnIce)
        {
            enemyMovement.DefaultPatrol("Ice");
        }
        /*else
        {
            ChangeFacingDirection();
        }*/
        
    }

    protected override void ChasePlayer()
    {
        if (curTimeBtwShot > timeBtwShot && animationManager.currentState != "HockeyPlayer_shoot")
        {
            animationManager.ChangeAnimation("shoot");
            ShootProjectile();
            curTimeBtwShot = 0;
        }
        else
        {
            curTimeBtwShot += Time.deltaTime;
        }
    }

    void ShootProjectile()
    {
        Vector2 shotPos = projectileShooter.ShotPos.position;
        Vector2 direction = MathUtils.GetXDirection(shotPos, player.GetPosition());
        float distance = MathUtils.GetAbsXDistance(shotPos, player.GetPosition());
        direction.x = shotPos.x + (facingDirection == RIGHT? distance : -distance);// new Vector2(shotPos.x * (shotPos.x - player.GetPosition().x), 0f);

        projectileShooter.ShootProjectileAndSetDistance(direction , "Ice");
        animationManager.SetCurrentState("idle", true);
    }


    public void projectileShooter_ProjectileTouchedPlayer()
    {
        //player.Push((facingDirection == RIGHT? pushForce.x : -pushForce.x), pushForce.y);
        Vector2 direction = new Vector2(facingDirection == RIGHT? pushVector.x : -pushVector.x, pushVector.y);
        player.Knockback(pushTime, pushForce, direction);
    }

}
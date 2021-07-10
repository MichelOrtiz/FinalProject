using UnityEngine;
public class HockeyPlayer : Bear, IProjectile
{
    [SerializeField] private Transform shotPos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float startTimeBtwShot;
    [SerializeField] private LayerMask whatIsIce;
    [SerializeField] private float pushForce;
    [SerializeField] private float maxViewDistance;
    private Projectile projectile;
    private float timeBtwShot;
    private bool isOnIce;
    
    new void Start()
    {
        base.Start();
    }
    new void Update()
    {
        //isOnIce = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsIce);
        
        // change "Ground" to "Obtacles" maybe
        /*RaycastHit2D hit = Physics2D.Linecast(fovOrigin.position, fovOrigin.position + (facingDirection == RIGHT? Vector3.right : Vector3.left) * maxViewDistance, 1 << LayerMask.NameToLayer("Ground"));
        if (hit.collider == null)
        {
            viewDistance = maxViewDistance;
        }
        else 
        {
            viewDistance = hit.distance;
        }*/
        //fieldOfView.SetViewDistanceOnRayHitObstacle(facingDirection == RIGHT? Vector3.right : Vector3.left, maxViewDistance);
        base.Update();
    }

    protected override void MainRoutine()
    {
        if (isOnIce)
        {
            if (InFrontOfObstacle() || IsNearEdge())
            {
                if (waitTime > 0)
                {
                    isWalking = false;
                    waitTime -= Time.deltaTime;
                    return;
                }
                ChangeFacingDirection();
                waitTime = startWaitTime;
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * normalSpeed);
                isWalking = true;
            }
        }
        else
        {
            if (waitTime > 0)
            {
                isWalking = false;
                waitTime -= Time.deltaTime;
                return;
            }
            ChangeFacingDirection();
            waitTime = startWaitTime;
        }
        
    }

    protected override void ChasePlayer()
    {
        if (timeBtwShot <= 0)
        {
            ShotProjectile(shotPos, new Vector3(player.feetPos.position.x, shotPos.transform.position.y));
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        return;
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
        player.Push((facingDirection == RIGHT? -pushForce : pushForce), 0f);
        
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this, "Ice"); // change to "Ice" when exists
    }

    void OnCollisionStay2D(Collision2D other)
    {
        isOnIce = other.collider.tag == "Ice";// change to "Ice" when exists
    }

}
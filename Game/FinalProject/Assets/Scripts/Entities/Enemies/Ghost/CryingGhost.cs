using UnityEngine;
using System.Collections;
public class CryingGhost : Ghost, IProjectile
{
    [SerializeField] private Transform shotPos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float startTimeBtwShot;
    private Projectile projectile;
    private float timeBtwShot;

    
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        rigidbody2d.position = Vector3.MoveTowards(GetPosition(), new Vector2(player.GetPosition().x, GetPosition().y), chaseSpeed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Linecast(shotPos.position, shotPos.position + Vector3.down * fieldOfView.FovAngle, 1 << LayerMask.NameToLayer("Ground"));
        if (timeBtwShot <= 0)
        {
            ShotProjectile(shotPos, hit.point);
            //ShotProjectile(shotPos, Vector3.down + shotPos.transform.pos);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

    IEnumerator WhileChasingRoutine()
    {
        
        return null;
    }
    
    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalBossEnemy : NormalType, ILaser
{
    [SerializeField] private Transform shotPos;
    [SerializeField] private float minDistanceToShotRay;
    [SerializeField] private float intervalToShot;
    //[SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float laserDamage;
    [SerializeField] private float laserSpeed;
    private float timeToShot;
    private float distanceToPlayer;
    private Laser laser;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        laserSpeed *= averageSpeed;
        //ShootLaser(shotPos.position, player.GetPosition());
    }

    // Update is called once per frame
    new void Update()
    {
        if (InFrontOfObstacle() ||( (GetPosition().x > player.GetPosition().x && facingDirection == RIGHT)
            || GetPosition().x < player.GetPosition().x && facingDirection == LEFT) )
            {
                ChangeFacingDirection();
            }
        base.Update();
    }


    public override void ConsumeItem(Item item)
    {
        return;
    }

    protected override void Attack()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        distanceToPlayer = Vector2.Distance(GetPosition(), player.GetPosition());
        if (distanceToPlayer >= minDistanceToShotRay)
        {
            if (timeToShot > intervalToShot)
            {
                if (laser == null)
                {
                    ShootLaser(shotPos.position, player.GetPosition());
                }


                timeToShot = 0;
            }
            else
            {
                timeToShot += Time.deltaTime;
            }
            
        }
        else
        {
            timeToShot = 0;
        }
    }

    protected override void MainRoutine()
    {
        return;
    }


    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, laserSpeed, this);
    }
    public void LaserAttack()
    {
        player.TakeTirement(laserDamage);
    }
}

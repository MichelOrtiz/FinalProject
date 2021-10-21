using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Entity
{
    [Header("Self Additions")]
    [SerializeField] private float damageAmount;
    [Header("Speed Params")]
    [SerializeField] private float baseSpeedMultiplier;
    [SerializeField] private float speedChangeMultiplier;
    private float speed;
    [SerializeField] private float intervalToChangeSpeed;
    private float currentTimeUntilChange;
    [SerializeField] private float changeSpeedTimeAcive;
    private float currentChangedSpeedTime;
    private bool speedChanged;

    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private LaserShooter laserShooter;
    [SerializeField] private float intervalToShoot;
    private float timeToShoot;


    #endregion

    


    /* change to enemy and inherit later*/
    private EnemyCollisionHandler eCollisionHandler;

    private PlayerManager player;
    /*-----------------------------------*/

    new void Awake()
    {
        base.Awake();
        eCollisionHandler = base.collisionHandler as EnemyCollisionHandler;
        eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_Attack;
    }


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        speed = averageSpeed * baseSpeedMultiplier;

    }

    // Update is called once per frame
    new void Update()
    {
        HandleSpeed();
        HandleLaser();
        base.Update();
    }

    void FixedUpdate()
    {
        if (GetPosition().y > player.GetPosition().y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }


    void HandleSpeed()
    {
        if (currentTimeUntilChange > intervalToChangeSpeed)
        {
            if (!speedChanged)
            {
                speed = averageSpeed * speedChangeMultiplier;
                speedChanged = true;
            }
            if (currentChangedSpeedTime > changeSpeedTimeAcive)
            {
                speed = averageSpeed * baseSpeedMultiplier;
                currentChangedSpeedTime = 0;
                currentTimeUntilChange = 0;
                speedChanged = false;
            }
            else
            {
                currentChangedSpeedTime += Time.deltaTime;
            }
        }
        else
        {
            currentTimeUntilChange += Time.deltaTime;
        }
    }

    void HandleLaser()
    {
        if (timeToShoot > intervalToShoot)
        {
            Vector2 newShotPosition =  new Vector2(player.GetPosition().x, laserShooter.ShotPos.position.y);
            laserShooter.ShootLaser(player.GetPosition());
            timeToShoot = 0;
        }
        else
        {
            timeToShoot += Time.deltaTime;
        }
    }

    void eCollisionHandler_Attack()
    {
        player.TakeTirement(damageAmount);
        player.SetImmune();
    }
}

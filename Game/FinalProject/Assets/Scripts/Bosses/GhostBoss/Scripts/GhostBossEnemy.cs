using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBossEnemy : Entity, IProjectile
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject projectilePrefab;
    private SeekerProjectile projectile;
    [SerializeField] private State projectileEffectOnPlayer;

    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;
    
    [SerializeField] private float timeBeforeStart;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float damageAmount;
    private bool touchingPlayer;

    private PlayerManager player;
    
    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        speedMultiplier *= averageSpeed;
    }

    new void Update()
    {
        if (timeBeforeStart > 0)
        {
            timeBeforeStart -= Time.deltaTime;
        }
        else
        {
            if (curTimeBtwShot > timeBtwShot)
            {
                ShotProjectile(shotPoint, player.GetPosition());
                curTimeBtwShot = 0;
            }
            else
            {
                curTimeBtwShot += Time.deltaTime;
            }
        }
        base.Update();
    }

    void FixedUpdate()
    {
        if (timeBeforeStart <= 0)
        {
            ChasePlayer();
        }

    }
    void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), player.GetPosition(), speedMultiplier * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = true;

            if (player.GetComponent<StatesManager>().currentStates.Contains(projectileEffectOnPlayer))
            {
                projectileEffectOnPlayer.StopAffect();
                //player.GetComponent<StatesManager>().RemoveState(projectileEffectOnPlayer);
                player.currentStamina = 0;
            }
            else
            {
                player.TakeTirement(damageAmount);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    public void ProjectileAttack()
    {
        player.GetComponent<StatesManager>().AddState(projectileEffectOnPlayer);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<SeekerProjectile>();
        projectile.Setup(from, player.transform, this);
    }
}

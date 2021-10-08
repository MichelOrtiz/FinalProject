using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBossEnemy : Entity
{
    [Header("Params")]
    [SerializeField] private float timeBeforeStart;
    private float curTime;
    [SerializeField] private float speedMultiplier;
    private float speed;
    [SerializeField] private float damageAmount;
    private bool touchingPlayer;

    private PlayerManager player;

    [Header("Division")]
    [SerializeField] private Transform divisionPoint;
    [SerializeField] private float maxDivisions;
    private float currentDivisions;
    private bool alreadyDivided;
    [SerializeField] private float timeUntilDivide;
    private float timeInLight;
    [SerializeField] private float speedIncrease;
    [SerializeField] private float projectileSpeedIncrease;
    
    // Maybe temporary
    [SerializeField] private float sizeDecreaseMultiplier;


    private bool InLight;

    [Header("Projectile stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private GameObject seekerProj;
    [SerializeField] private GameObject bouncingProj;

    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;


    [Header("References")]
    [SerializeField] private EnemyMovement enemyMovement;
    private GameObjectCloner gameObjectCloner;
    [SerializeReference] private LightZone currentLZ;
    private EnemyCollisionHandler enemyCollisionHandler;
    
    new void Awake()
    {
        gameObjectCloner = GetComponent<GameObjectCloner>();
        enemyCollisionHandler = collisionHandler as EnemyCollisionHandler;
        enemyCollisionHandler.TouchedPlayerHandler += eCollisionHandler_Attack;
    }


    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        speed = averageSpeed * speedMultiplier;

        curTimeBtwShot = 0;
        timeInLight = 0;
    }

    new void Update()
    {
        if (timeBeforeStart > 0)
        {
            timeBeforeStart -= Time.deltaTime;
        }
        else
        {
            if (( facingDirection == RIGHT && player.GetPosition().x < GetPosition().x )
                || ( facingDirection == LEFT && player.GetPosition().x > GetPosition().x ))
            {
                transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180); 
            }

            if (curTimeBtwShot > timeBtwShot)
            {
                //ShotProjectile(shotPoint, player.GetPosition());
                projectileShooter.ShootSeekerProjectile(player.transform);
                curTimeBtwShot = 0;
            }
            else
            {
                curTimeBtwShot += Time.deltaTime;
            }

            
        }
        if (InLight)
        {
            if (timeInLight > timeUntilDivide)
            {
                Divide();
                InLight = false;
                timeInLight = 0;

                //currentLZ?.UnableDoor();
            }
            else
            {
                timeInLight += Time.deltaTime;
            }
        }

        

        base.Update();
    }

    void FixedUpdate()
    {
        if (timeBeforeStart <= 0)
        {
            enemyMovement.GoTo(player.GetPosition(), chasing: true, gravity: false);
            //ChasePlayer();
        }

    }
    void ChasePlayer()
    {
        if (!touchingPlayer)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), player.GetPosition(), speedMultiplier * Time.deltaTime);
        }
    }

    void eCollisionHandler_Attack()
    {
        var effect = projectileShooter.EffectOnPlayer;
        if (player.statesManager.currentStates.Contains(effect))
        {
            effect.StopAffect();
            player.currentStamina = 0;
        }
        else
        {
            player.TakeTirement(damageAmount);
            player.SetImmune();    
        }
    }

    protected override void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.tag == "Light")
        {
            InLight = true;
            currentLZ = contact.transform?.parent?.GetComponentInChildren<LightZone>();

            

            // reset time, so the enemy doesn't chase
            //curTime = 0;
        }
    }

    protected override void collisionHandler_ExitContact(GameObject contact)
    {
        if (contact.tag == "Light")
        {
            InLight = false;

            //timeInLight = 0;
        }
    }

    void Divide()
    {
        if (currentDivisions < maxDivisions)
        {

            transform.localScale *= sizeDecreaseMultiplier;
        
            //speedMultiplier += speedIncrease;

            enemyMovement.ChaseSpeed += speedIncrease;
            curTime = timeBeforeStart;
            //projectileShooter.ProjectileFromPrefab.speedMultiplier += projectileSpeedIncrease;

            currentDivisions++;
            gameObjectCloner.Divide(checkMax: true);
            
        }
        else
        {
            DestroyEntity();
        }
    }
    void ShootBouncingProj()
    {
        SetProjectile(bouncingProj);
        projectileShooter.ShootProjectile(player.GetPosition());
    }

    void SetProjectile(GameObject projectile)
    {
        projectileShooter.projectilePrefab = projectile;
    }
}

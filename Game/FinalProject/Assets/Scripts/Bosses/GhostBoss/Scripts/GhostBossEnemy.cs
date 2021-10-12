using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBossEnemy : Entity
{
    [Header("Params")]
    [SerializeField] private float timeBeforeStart;
    private float curTime;
    [SerializeField] private float speedModifierInLight;
    [SerializeField] private float damageAmount;
    private bool touchingPlayer;

    private PlayerManager player;

    [Header("Division")]
    [SerializeField] private Transform divisionPoint;
    [SerializeField] private float maxDivisions;
    private float currentDivisions;
    private List<GameObject> divisions = new List<GameObject>();
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
    private GhostBoss ghostBoss;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    new void Awake()
    {
        gameObjectCloner = GetComponent<GameObjectCloner>();
        enemyCollisionHandler = collisionHandler as EnemyCollisionHandler;
        enemyCollisionHandler.TouchedPlayerHandler += eCollisionHandler_Attack;

        divisions.Add(gameObject);
    }


    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        ghostBoss = FindObjectOfType<GhostBoss>();


        curTimeBtwShot = 0;
        timeInLight = 0;

        spriteRenderer.color = Color.white;
    }

    new void Update()
    {
        if (timeBeforeStart > 0)
        {
            timeBeforeStart -= Time.deltaTime;
        }
        else if(!ghostBoss.inPush)
        {
            if (( facingDirection == RIGHT && player.GetPosition().x < GetPosition().x )
                || ( facingDirection == LEFT && player.GetPosition().x > GetPosition().x ))
            {
                transform.eulerAngles = new Vector3(0, facingDirection == LEFT? 0:180); 
            }

            if (curTimeBtwShot > timeBtwShot)
            {
                //ShotProjectile(shotPoint, player.GetPosition());
                projectileShooter.ShootProjectile(player.GetPosition());
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
                spriteRenderer.color = Color.white;
                //currentLZ?.UnableDoor();
            }
            else
            {
                spriteRenderer.color =  Color.Lerp(Color.white, Color.red, timeInLight / timeUntilDivide);
                timeInLight += Time.deltaTime;
            }
        }

        

        base.Update();
    }

    void FixedUpdate()
    {
        if (timeBeforeStart <= 0)
        {
            if (!ghostBoss.inPush)
            {
                enemyMovement.GoTo(player.GetPosition(), chasing: false, gravity: false);
            
            }
            //ChasePlayer();
        }

    }

    void eCollisionHandler_Attack()
    {
        var effect = projectileShooter.EffectOnPlayer;
        /*if (player.statesManager.currentStates.Contains(effect))
        {
            effect.StopAffect();
            player.currentStamina = 0;
        }
        else*/
        //{
            player.TakeTirement(damageAmount);
            player.currentStaminaLimit -= 10;
            player.SetImmune();
        //}
    }

    protected override void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.tag == "Light")
        {
            InLight = true;
            currentLZ = contact.transform?.parent?.GetComponentInChildren<LightZone>();

            
            enemyMovement.DefaultSpeed *= speedModifierInLight;
            // reset time, so the enemy doesn't chase
            //curTime = 0;
        }
        if (contact.tag != "Platform" && contact.tag != "Boundary" && GroundChecker.GroundTags.Contains(contact.tag))
        {
            rigidbody2d.velocity = new Vector2();
            ghostBoss.StopPush();
        }
    }

    protected override void collisionHandler_ExitContact(GameObject contact)
    {
        if (contact.tag == "Light")
        {
            InLight = false;

            enemyMovement.DefaultSpeed /= speedModifierInLight;


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

            //direction = (player.GetPosition() - GetPosition()).normalized;
            //target = new Vector2(direction.x, direction.y);

            

            var obj = Instantiate(this, divisionPoint.position, transform.rotation);
            obj.currentDivisions = currentDivisions;

            divisions.Add(obj.gameObject);
            ghostBoss.StartPush(transform, divisions);



            /*gameObjectCloner.sourceObject = gameObject;
            gameObjectCloner.sourceObject.GetComponent<GhostBossEnemy>().currentDivisions = currentDivisions;
            gameObjectCloner.Divide(checkMax: true);*/
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

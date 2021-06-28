using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBossEnemy : Entity, IProjectile
{
    #region Params
    [Header("Params")]
    [SerializeField] private float timeBeforeStart;
    [SerializeField] private float speedMultiplier;
    private float speed;
    [SerializeField] private float damageAmount;
    private bool touchingPlayer;

    private PlayerManager player;
    #endregion

    #region Division
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
    #endregion
    
    #region Projectile Stuff
    [Header("Projectile stuff")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject projectilePrefab;
    private SeekerProjectile projectile;
    [SerializeField] private State projectileEffectOnPlayer;

    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;
    #endregion
    
    #region References
    [SerializeReference] private LightZone currentLZ;

    #endregion
    
    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        speed = averageSpeed * speedMultiplier;
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
                ShotProjectile(shotPoint, player.GetPosition());
                curTimeBtwShot = 0;
            }
            else
            {
                curTimeBtwShot += Time.deltaTime;
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
                player.currentStamina = 0; //insta kill??
            }
            else
            {
                player.TakeTirement(damageAmount);
            }
        }

        if (other.tag == "Light")
        {
            InLight = true;
            currentLZ = other.transform.parent.GetComponentInChildren<LightZone>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = false;
        }

        if (other.tag == "Light")
        {
            InLight = false;
        }
    }

    void Divide()
    {
        if (currentDivisions < maxDivisions)
        {
            if (!alreadyDivided)
            {


                transform.localScale *= sizeDecreaseMultiplier;
            
                speedMultiplier += speedIncrease;

                if (projectile != null)
                {
                    projectile.speedMultiplier += projectileSpeedIncrease;
                }

                currentDivisions++;
                Instantiate(this, divisionPoint.position, Quaternion.identity).currentDivisions = currentDivisions;



                //alreadyDivided = true;
            }
        }
        else
        {
            Destroy(gameObject);
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

    void OnDestroy()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Rigids, colliders, layers, etc
    [Header("Rigids, colliders, layers, etc")]
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [SerializeField] protected LayerMask whatIsObstacle;
    [SerializeField] protected GameObject warning;
    [SerializeField] protected GameObject? impactEffect;

    [SerializeField] protected bool targetWarningAvailable;
    //[SerializeField] protected bool collidesWithPlayer;
    [SerializeField] protected bool independentAttackEnabled;
    protected LayerMask defaultLayer;
    [SerializeField] private CollisionHandler collisionHandler;
    #endregion

    #region Main Params
    [Header("MainParams")]
    [SerializeField] public float speedMultiplier;
    [SerializeField] protected float maxShotDistance;
    public float MaxShotDistance
    {
        get { return maxShotDistance; }
        set { maxShotDistance = value; }
    }
    
    public float damage;
    // Time until destroyed
    [SerializeField] protected float waitTime;
    [SerializeField] protected float impactEffectExitTime;
    public bool touchingPlayer;
    public bool touchingObstacle;
    #endregion

    #region Misc
    [Header("Size")]
    [SerializeField] protected bool changeSizeByDistance;
    [SerializeField] protected float distanceToIncrease;
    protected float currentDistanceToSize;
    [SerializeField] protected float sizeByDistanceMultiplier;
    [SerializeField] protected bool changeSizeByTime;
    [SerializeField] protected float timeToIncrease;
    protected float currentTimeToSize;
    [SerializeField] protected float sizeByTimeMultiplier;


    protected Vector2 distance;
    protected Vector3 startPoint;
    protected IProjectile enemy;
    public Vector3 shootDir { get; set; }
    private Vector3 target;
    protected string colliderTag;
    [SerializeReference]protected bool isOnCollider;

    protected bool aboutToDestroy;

    //private Animator animator;
    #endregion

    public void Setup(Transform startPoint, Vector3 target)
    {
        this.startPoint = startPoint.position;
        this.target = target;
        shootDir = (target - startPoint.position).normalized;
    }

    public void Setup(Transform startPoint, Vector3 target, IProjectile enemy)
    {
        this.startPoint = startPoint.position;
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint.position).normalized;
    }

    public void Setup(Transform startPoint, Vector3 target, IProjectile enemy, string colliderTag)
    {
        this.startPoint = startPoint.position;
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint.position).normalized;
        this.colliderTag = colliderTag;
    }

    /*  - - - - - - - - --*/

    public void Setup(Vector2 startPoint, Vector2 target)
    {
        this.startPoint = startPoint;
        this.target = target;
        shootDir = (target - startPoint).normalized;
    }

    public void Setup(Vector2 startPoint, Vector2 target, IProjectile enemy)
    {
        this.startPoint = startPoint;
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint).normalized;
    }

    public void Setup(Vector2 startPoint, Vector2 target, IProjectile enemy, string colliderTag)
    {
        this.startPoint = startPoint;
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint).normalized;
        this.colliderTag = colliderTag;
    }
    /* - - - - - - - */
    void Start()
    {
        defaultLayer = gameObject.layer;
        if (impactEffect != null)
        {
            impactEffect.SetActive(false);
        }
        speedMultiplier *= Entity.averageSpeed;
        if (targetWarningAvailable)
        {
            Instantiate(warning, target, Quaternion.identity);
        }
    }

    void Update()
    {
        isOnCollider = colliderTag != null && !collisionHandler.Contacts.Exists( c=> c.tag != colliderTag) && collisionHandler.Contacts.Exists( c=> c.tag == colliderTag);
        //rigidbody2d.position = Vector3.MoveTowards(transform.position, shootDir, speedMultiplier * Time.deltaTime); 
        //animator.SetBool("Is Destroying", aboutToDestroy);
        Vector2 distance = startPoint - transform.position;
        
        // Hipotenusa
        float hipotenusa = Mathf.Sqrt((distance.x * distance.x) + (distance.y * distance.y));
        if(hipotenusa > maxShotDistance)
        {
            Destroy();
        }
        
        if (touchingObstacle || (colliderTag != null && !isOnCollider))
        {
            // maybe temporary
            rigidbody2d.gravityScale = 0;
            speedMultiplier = 0;
            rigidbody2d.velocity = new Vector2();
            // *


            if (waitTime <= 0)
            {
                Destroy();
            }
            else
            {
                waitTime -= Time.deltaTime;
            } 
        }
        
        if (changeSizeByTime)
        {
            ChangeSizeByTime();
        }
        
    }
    
    protected void FixedUpdate()
    {
        if (!touchingPlayer)
        {
            if (colliderTag == null)
            {
                if (!touchingObstacle)
                {
                    transform.position += shootDir * speedMultiplier * Time.deltaTime *(rigidbody2d.gravityScale != 0? rigidbody2d.gravityScale : 1);
                }
            }
            else
            {
                if (isOnCollider)
                {
                    transform.position += shootDir * speedMultiplier * Time.deltaTime *(rigidbody2d.gravityScale != 0? rigidbody2d.gravityScale : 1);
                }
            }
        }
    }

    /*private void ChangeSizeByDistance()
    {
        float currentDistance = Vector2.Distance(transform.position, startPoint.position);
        if (currentDistanceToSize >= currentDistance + distanceToIncrease)
        {
            transform.localScale *= sizeByDistanceMultiplier;
            currentDistanceToSize = 0;
        }
        else
        {
            currentDistanceToSize = 
        }
    }*/

    protected void ChangeSizeByTime()
    {
        if (currentTimeToSize> timeToIncrease)
        {
            transform.localScale = transform.localScale + transform.localScale * sizeByTimeMultiplier;
            currentTimeToSize = 0;
        }
        else
        {
            currentTimeToSize += Time.deltaTime;
        }
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        touchingPlayer = other.gameObject.tag == "Player";

        if (touchingPlayer)
        {
             if (independentAttackEnabled)
            {
                PlayerManager.instance.TakeTirement(damage);
            }
            aboutToDestroy = true;
            if (enemy != null)
            {
                enemy.ProjectileAttack();
            }
            Destroy();
        }
        //touchingObstacle = other.gameObject.layer == whatIsObstacle;

        /*if (other.gameObject.layer == whatIsObstacle)
        {

        }*/

        touchingObstacle = Physics2D.OverlapCircle(transform.position, GetComponent<Collider2D>().bounds.extents.magnitude, whatIsObstacle);
        //touchingObstacle = other.gameObject.layer == whatIsObstacle;
    }

    
    protected void OnTriggerStay2D(Collider2D other)
    {
        //isOnCollider = other.gameObject.tag == colliderTag;
    }
    

    protected void OnCollisionEnter2D(Collision2D other)
    {
        touchingPlayer = other.gameObject.tag == "Player";
        //touchingObstacle = other.gameObject.layer == whatIsObstacle;
        //touchingObstacle = other.gameObject.layer == whatIsObstacle;
        touchingObstacle = Physics2D.OverlapCircle(transform.position, GetComponent<Collider2D>().bounds.extents.magnitude, whatIsObstacle);
        //touchingObstacle = other.gameObject.layer == whatIsObstacle;

    }
    
    protected void OnCollisionStay2D(Collision2D other)
    {
       // isOnCollider = other.gameObject.tag == colliderTag;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            gameObject.layer = defaultLayer;
        }
        /*else if(other.gameObject.layer == whatIsObstacle)
        {
            touchingObstacle = false;
        }*/
        else if (other.gameObject.tag == "Ground")
        {
            touchingObstacle = false;
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            gameObject.layer = defaultLayer;
        }
        /*else if(other.gameObject.layer == whatIsObstacle)
        {
            touchingObstacle = false;
        }*/
        else if (other.gameObject.tag == "Ground")
        {
            touchingObstacle = false;
        }
    }

    public void Destroy()
    {
        if (impactEffect != null)
        {
            impactEffect.SetActive(true);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
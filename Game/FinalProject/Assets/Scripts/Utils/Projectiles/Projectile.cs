using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Rigids, colliders, layers, etc
    [Header("Rigids, colliders, layers, etc")]
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject? impactEffect;

    [SerializeField] private bool targetWarningAvailable;
    [SerializeField] private bool collidesWithPlayer;
    private LayerMask defaultLayer;
    #endregion

    #region Main Params
    [Header("MainParams")]
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maxShotDistance;
    public float damage;
    // Time until destroyed
    [SerializeField] private float waitTime;
    [SerializeField] private float impactEffectExitTime;
    public bool touchingPlayer;
    public bool touchingObstacle;
    #endregion

    #region Misc
    [Header("Size")]
    [SerializeField] private bool changeSizeByDistance;
    [SerializeField] private float distanceToIncrease;
    private float currentDistanceToSize;
    [SerializeField] private float sizeByDistanceMultiplier;
    [SerializeField] private bool changeSizeByTime;
    [SerializeField] private float timeToIncrease;
    private float currentTimeToSize;
    [SerializeField] private float sizeByTimeMultiplier;


    private Vector2 distance;
    private Vector3 startPoint;
    private IProjectile enemy;
    private Vector3 shootDir;
    private Vector3 target;
    private string colliderTag;
    private bool isOnCollider;

    private bool aboutToDestroy;

    //private Animator animator;
    #endregion

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
        //rigidbody2d.position = Vector3.MoveTowards(transform.position, shootDir, speedMultiplier * Time.deltaTime); 
        //animator.SetBool("Is Destroying", aboutToDestroy);
        Vector2 distance = startPoint - transform.position;
        
        // Hipotenusa
        float hipotenusa = Mathf.Sqrt((distance.x * distance.x) + (distance.y * distance.y));
        if(hipotenusa > maxShotDistance)
        {
            Destroy();
        }
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
        else
        {
            FixedUpdate();
        }
        
        if (touchingObstacle)
        {
            rigidbody2d.Sleep();
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
    
    void FixedUpdate()
    {
        if (touchingPlayer)
        {
                
                aboutToDestroy = true;
                enemy.ProjectileAttack();
                Destroy();
            
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

    private void ChangeSizeByTime()
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        touchingPlayer = other.gameObject.tag == "Player";
        touchingObstacle = other.gameObject.layer == whatIsObstacle;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        isOnCollider = other.gameObject.tag == colliderTag;
    }
    

    void OnCollisionEnter2D(Collision2D other)
    {
        touchingPlayer = other.gameObject.tag == "Player";
        touchingObstacle = other.gameObject.layer == whatIsObstacle;
        //touchingObstacle = other.gameObject.layer == whatIsObstacle;
    }
    
    void OnCollisionStay2D(Collision2D other)
    {
        isOnCollider = other.gameObject.tag == colliderTag;
    }

    void OnTriggerExit2D(Collider2D other)
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

    void OnCollisionExit2D(Collision2D other)
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
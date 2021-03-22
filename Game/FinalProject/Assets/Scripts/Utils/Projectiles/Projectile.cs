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
    [SerializeField] private bool targetWarningAvailable;
    #endregion

    #region Main Params
    [Header("MainParams")]
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maxShotDistance;
    public float damage;
    // Time until destroyed
    [SerializeField] private float waitTime;
    public bool touchingPlayer;
    public bool touchingObstacle;
    #endregion

    #region Misc
    private IProjectile enemy;
    private Vector3 shootDir;
    private Vector3 target;
    private string colliderTag;
    private bool isOnCollider;
    #endregion

    public void Setup(Transform startPoint, Vector3 target, IProjectile enemy)
    {
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint.position).normalized;
    }

    public void Setup(Transform startPoint, Vector3 target, IProjectile enemy, string colliderTag)
    {
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint.position).normalized;
        this.colliderTag = colliderTag;
    }

    void Start()
    {
        speedMultiplier *= Entity.averageSpeed;
        if (targetWarningAvailable)
        {
            Instantiate(warning, target, Quaternion.identity);
        }
    }

    void Update()
    {
        //rigidbody2d.position = Vector3.MoveTowards(transform.position, shootDir, speedMultiplier * Time.deltaTime); 
        
        if (colliderTag == null)
        {
            transform.position += shootDir * speedMultiplier * Time.deltaTime *(rigidbody2d.gravityScale != 0? rigidbody2d.gravityScale : 1);
        }
        else
        {
            if (isOnCollider)
            {
                transform.position += shootDir * speedMultiplier * Time.deltaTime *(rigidbody2d.gravityScale != 0? rigidbody2d.gravityScale : 1);
            }
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
        if (touchingPlayer)
        {
            enemy.ProjectileAttack();
            Destroy();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        touchingPlayer = other.gameObject.tag == "Player";
        touchingObstacle = other.gameObject.tag == "Ground";
    }

    void OnTriggerStay2D(Collider2D other)
    {
        isOnCollider = other.gameObject.tag == colliderTag;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        touchingPlayer = other.gameObject.tag == "Player";
        touchingObstacle = other.gameObject.tag == "Ground";
    }

    void OnCollisionStay2D(Collision2D other)
    {
        isOnCollider = other.gameObject.tag == colliderTag;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
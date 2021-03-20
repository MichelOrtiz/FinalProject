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
    #endregion

    public void Setup(Transform startPoint, Vector3 target, IProjectile enemy)
    {
        this.target = target;
        this.enemy = enemy;
        shootDir = (target - startPoint.position).normalized;
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
        
        transform.position += shootDir * speedMultiplier * Time.deltaTime;
        
        if (touchingObstacle)
        {
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

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
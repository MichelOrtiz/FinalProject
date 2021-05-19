using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For projectiles that don't fire when instantiated
/// </summary>
public class StaticBullet : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private State effectOnPlayer;
    
    [SerializeField] private bool activateOnPlayerOnRadius;
    [SerializeField] private float radius;
    
    [SerializeField] private bool activateBasedOnTime;
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;

    public Projectile Projectile { get; set; }
    private PlayerManager player;
    

    /*public delegate void PlayerOnRadius();
    public event PlayerOnRadius PlayerOnRadiusHandler;
    protected virtual void OnPlayerOnRadius()
    {
        playerOnRadius = true;
        PlayerOnRadiusHandler?.Invoke();
    }*/
    private bool playerOnRadius;

    void Start()
    {
        Projectile = GetComponent<Projectile>();    
        Projectile.enabled = false;

        player = PlayerManager.instance;
    }


    // Update is called once per frame
    void Update()
    {
        if (activateOnPlayerOnRadius)
        {
            HandlePlayerOnRadius();
        }
        if (activateBasedOnTime)
        {
            HandleBasedOnTime();
        }


    }

    void HandlePlayerOnRadius()
    {
        if (!playerOnRadius)
        {
            if (Vector2.Distance(player.GetPosition(), transform.position) <= radius)
            {
                playerOnRadius = true;
                ActivateBullet(player.GetPosition());
                //OnPlayerOnRadius();
            } 
            else
            {
                playerOnRadius = false;
            }
        }
    }

    void HandleBasedOnTime()
    {
        if (curTimeBtwShot > timeBtwShot)
        {
            ActivateBullet(player.GetPosition());
            curTimeBtwShot = 0;
        }
        else
        {
            curTimeBtwShot += Time.deltaTime;
        }
    }
    
    /// <summary>
    /// Activates the bullet from its current position
    /// </summary>
    /// <param name="to"></param>
    public void ActivateBullet(Vector2 to)
    {
        Projectile.Setup(Projectile.transform.position, to);
        Projectile.enabled = true;
    }

    public void ActivateBullet(Vector2 from, Vector2 to)
    {
        Projectile.Setup(from, to);
        Projectile.enabled = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.TakeTirement(damageAmount);
            
            if (effectOnPlayer != null)
            {
                player.statesManager.AddState(effectOnPlayer);
            }

            Destroy(gameObject);
        }
    }

}

using UnityEngine;
using System.Collections.Generic;
public class EnemyCollisionHandler : CollisionHandler
{
    private PlayerManager player;
    public bool touchingPlayer;
    public bool touchingEnemy;
    public bool touchingGround;
    public Enemy lastEnemyTouched;

    public delegate void TouchedPlayer();
    public event TouchedPlayer TouchedPlayerHandler;
    protected virtual void OnTouchedPlayer()
    {
        TouchedPlayerHandler?.Invoke();
    }

    public delegate void TouchingPlayer();
    public event TouchingPlayer TouchingPlayerHandler;
    protected virtual void OnTouchingPlayer()
    {
        TouchingPlayerHandler?.Invoke();
    }

    public delegate void StoppedTouchingPlayer(); 
    public event StoppedTouchingPlayer StoppedTouchingHandler;
    protected virtual void OnStoppedTouchingPlayer()
    {
        StoppedTouchingHandler?.Invoke();
    }

    public delegate void TouchingGround();
    public event TouchingGround TouchingGroundHandler;
    protected void OnTouchingGround(){
        TouchingGroundHandler?.Invoke();
    }
    
    public delegate void TouchedEnemy(Enemy enemy);
    public event TouchedEnemy TouchedEnemyHandler;
    protected virtual void OnTouchedEnemy(Enemy enemy)
    {
        TouchedEnemyHandler?.Invoke(enemy);
    }

    void Start()
    {
        player = PlayerManager.instance;
    }

    void Update()
    {
     
    }

    new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            OnTouchedPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            lastEnemyTouched = other.transform.parent.GetComponent<Enemy>();
            OnTouchedEnemy(lastEnemyTouched);
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
            OnTouchingGround();
        }
    }

    new void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            OnTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            
        }
        else if (other.gameObject.tag == "Ground")
        {
            
        }
    }
    
    new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            OnStoppedTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = false;

        }
    }

    new void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            OnTouchedPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            lastEnemyTouched = other.transform.parent.GetComponent<Enemy>();
            OnTouchedEnemy(lastEnemyTouched);
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
            OnTouchingGround();

        }
    }

 
    new void OnCollisionStay2D(Collision2D other)
    {
        base.OnCollisionStay2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            OnTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            
        }
        else if (other.gameObject.tag == "Ground")
        {
            
        }
    }

    new void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            OnStoppedTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = false;
        }
    }
}
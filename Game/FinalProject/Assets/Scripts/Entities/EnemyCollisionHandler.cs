using UnityEngine;
using System.Collections.Generic;
public class EnemyCollisionHandler : CollisionHandler
{
    private PlayerManager player;
    public bool touchingPlayer;
    public bool touchingEnemy;
    public bool touchingGround;
    public Enemy lastEnemyTouched;

    public delegate void AttackPlayer();
    public event AttackPlayer TouchingPlayer;
    protected virtual void OnTouchingPlayer()
    {
        TouchingPlayer?.Invoke();
    }

    public delegate void StopAttack(); 
    public event StopAttack JustTouchedPlayer;
    protected virtual void OnJustTouchedPlayer()
    {
        JustTouchedPlayer?.Invoke();
    }

    public delegate void TouchingGround();
    public event TouchingGround TouchingGroundHandler;
    protected void OnTouchingGround(){
        TouchingGroundHandler?.Invoke();
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
            OnTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            lastEnemyTouched = other.gameObject.GetComponent<Enemy>();
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
            OnTouchingGround();
        }
    }

    
    new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            OnJustTouchedPlayer();
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
            OnTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            lastEnemyTouched = other.transform.parent.GetComponent<Enemy>();
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
            OnTouchingGround();

        }
    }

    new void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            OnJustTouchedPlayer();
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
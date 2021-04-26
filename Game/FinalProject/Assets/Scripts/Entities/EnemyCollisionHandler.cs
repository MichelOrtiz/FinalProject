using UnityEngine;
using System.Collections.Generic;
public class EnemyCollisionHandler : MonoBehaviour
{
    //private Collider2D collider;
    //[SerializeField] private List<string> contacts;
    //public List<string> Contacts { get; private set; }

    [SerializeField] private List<string> contacts;
    public List<string> Contacts
    {
        get { return contacts; }
        private set { contacts = value; }
    }
    
    
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
    

    void Awake()
    {
        Contacts = new List<string>();
    }
    void Start()
    {
        player = PlayerManager.instance;
        
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        if (!contacts.Contains(other.gameObject.tag))
        {
            contacts.Add(other.gameObject.tag);
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
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
        if (contacts.Contains(other.gameObject.tag))
        {
            contacts.Remove(other.gameObject.tag);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            OnTouchingPlayer();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            lastEnemyTouched = other.gameObject.gameObject.GetComponent<Enemy>();
        }
        else if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
            OnTouchingGround();

        }
        if (!contacts.Contains(other.gameObject.tag))
        {
            contacts.Add(other.gameObject.tag);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
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
        if (contacts.Contains(other.gameObject.tag))
        {
            contacts.Remove(other.gameObject.tag);
        }
    }
}
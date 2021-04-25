using UnityEngine;
using System.Collections.Generic;
public class EnemyCollisionHandler : MonoBehaviour
{
    //private Collider2D collider;
    [SerializeField] private List<string> contacts;
    public List<string> Contacts { get; private set; }
    
    private PlayerManager player;
    public bool touchingPlayer;
    public bool touchingEnemy;
    public Enemy lastEnemyTouched;

    

    void Awake()
    {
        
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
        if (other.tag == "Player")
        {
            touchingPlayer = true;
        }
        else if (other.tag == "Enemy")
        {
            touchingEnemy = true;
            lastEnemyTouched = other.GetComponent<Enemy>();
        }
        if (!contacts.Contains(other.tag))
        {
            contacts.Add(other.tag);
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = false;
        }
        else if (other.tag == "Enemy")
        {
            touchingEnemy = false;
        }
        if (contacts.Contains(other.tag))
        {
            contacts.Remove(other.tag);
        }
    }
}
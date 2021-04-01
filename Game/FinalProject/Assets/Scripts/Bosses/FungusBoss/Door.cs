using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private enum Orientation
    {
        Vertical, 
        Horizontal
    }
    [SerializeField] private Orientation orientation;
    [SerializeField] private GameObject door;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] public bool isOpen;
    [SerializeField] private float pushForce;
    [SerializeField] private float radius;
    PlayerManager player;
    Enemy enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        //enemy = Enemy.instance;
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (isOpen)
        {
            if (player != null)
            {
                float distanceFromPlayer = Vector2.Distance(player.GetPosition(), transform.position);
                if (distanceFromPlayer <= radius)
                {
                    PushPlayer();
                }
            }
            /*if (enemy != null)
            {
                float distanceFromEnemy = Vector2.Distance(enemy.GetPosition(), transform.position);
                if (distanceFromEnemy <= radius)
                {
                    PushEnemy();
                }
            }*/
            
        }
    }

    /// <summary>
    /// Opens the door if closed. Closes if open.
    /// </summary>
    /// <returns></returns>
    public void Activate()
    {
        isOpen = !isOpen;
        UpdateState();
    }

    private void UpdateState()
    {
        if (isOpen)
        {
            door.GetComponent<BoxCollider2D>().enabled = false;
            door.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            door.GetComponent<BoxCollider2D>().enabled = transform;
            door.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void PushPlayer()
    {
        if (orientation == Orientation.Horizontal)
        {
            if (!player.isFalling)
            {
                player.Push(0f, pushForce);
            }
            else
            {
                player.Push(0f, -pushForce);
            }
        }
        else
        {
            if (player.facingDirection == "right")
            {
                player.Push(pushForce * 10, 0f);
            }
            else
            {
                player.Push(-pushForce * 10, 0f);
            }
        }
    }

    void PushEnemy()
    {
        if (orientation == Orientation.Horizontal)
        {
            if (!enemy.isFalling)
            {
                enemy.Push(0f, pushForce * 100);
            }
            else
            {
                enemy.Push(0f, pushForce * -100);
            }
        }
        else
        {
            if (enemy.facingDirection == "right")
            {
                enemy.Push(pushForce * 100, 0f);
            }
            else
            {
                enemy.Push(pushForce * -100, 0f);
            }
        }
    }
}

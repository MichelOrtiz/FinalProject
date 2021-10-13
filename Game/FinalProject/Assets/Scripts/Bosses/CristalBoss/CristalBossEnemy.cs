using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalBossEnemy : Entity
{
    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private float minDistanceToShotRay;
    [SerializeField] private float intervalToShot;
    private float timeToShot;

    //[SerializeField] private LineRenderer laser;
    [SerializeField] private LaserShooter laserShooter;

    #endregion

    #region RunFromPlayerParams
    [Header("Run from player")]
    [SerializeField] private float speed;
    [SerializeField] private float minDistanceToPlayer;
    private float distanceToPlayer;

    [SerializeField] private List<Vector2> platformPositions;
    private Vector2 currentPlatform;
    private Vector2 lastPlatform;
    private Vector2 nearestPlatform;
    private bool runningFromPlayer;
    private PlayerManager player;
    #endregion

    #region Player Interaction
    [Header("Player Interaction")]
    [SerializeField] private float interactionRadius;

    [SerializeField] private Color colorWhenInteractuable;
    [SerializeField] private Color colorWhenInteraction;
    private Color defaultColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private BlinkingSprite blinkingSprite;
    
    [SerializeField] private int interactionsToDestroy;
    private int interactions;

    private bool justInteracted;

    [SerializeField] private float cooldownAfterInteraction;
    private float curCooldown;

    #endregion

    new void Awake()
    {
        base.Awake();
        speed *= Entity.averageSpeed;
        blinkingSprite = GetComponent<BlinkingSprite>();
        blinkingSprite.enabled = false;

        defaultColor = spriteRenderer.color;

        
    }


    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        player.inputs.Interact += inputs_Interact;
        lastPlatform = GetPosition();
    }

    new void Update()
    {
        distanceToPlayer = Vector2.Distance(GetPosition(), player.GetPosition());

        if (runningFromPlayer)
        {
            if ((GetPosition().x > player.GetPosition().x && facingDirection == LEFT)
                || (GetPosition().x < player.GetPosition().x && facingDirection == RIGHT))
                {
                    ChangeFacingDirection();
                }
        }

        /*else
        {
            if (InFrontOfObstacle() ||( (GetPosition().x > player.GetPosition().x && facingDirection == LEFT)
            || GetPosition().x < player.GetPosition().x && facingDirection == RIGHT ))
            {
                ChangeFacingDirection();
            }
        }*/
        
        
        if (justInteracted)
        {
            if (curCooldown > cooldownAfterInteraction)
            {
                blinkingSprite.enabled = false;
                justInteracted = false;
                curCooldown = 0;

                if (interactions == interactionsToDestroy)
                {
                    DestroyEntity();
                }
            }
            else
            {
                curCooldown += Time.deltaTime;
            }
        }
        else
        {
            if (distanceToPlayer <= interactionRadius)
            {
                if (spriteRenderer.color != colorWhenInteractuable)
                {
                    spriteRenderer.color = colorWhenInteractuable;
                }
            }
            else
            {
                if (spriteRenderer.color != Color.white)
                {
                    spriteRenderer.color = Color.white;
                }
            }
        }
        base.Update();
    }

    void FixedUpdate()
    {
        if (distanceToPlayer >= minDistanceToShotRay)
        {
            if (timeToShot > intervalToShot)
            {
                if (laserShooter.Laser == null)
                {
                    laserShooter.ShootLaserAndSetEndPos(player.transform);
                }
                timeToShot = 0;
            }
            else
            {
                timeToShot += Time.deltaTime;
            }
            
        }
        else
        {
            timeToShot = 0;
        }

            if (distanceToPlayer <= minDistanceToPlayer)
            {
                if (nearestPlatform == new Vector2())
                {
                    nearestPlatform = FindNearestPlatform();
                    runningFromPlayer = true;
                }


            }
            if (runningFromPlayer)
            {
                rigidbody2d.position = Vector2.MoveTowards(GetPosition(), nearestPlatform, speed * Time.deltaTime);

                runningFromPlayer = (Vector2) GetPosition() != nearestPlatform;
            }        
            else
            {
                nearestPlatform = new Vector2();
            }
    }

    private Vector2 FindNearestPlatform()
    {
        Vector2 nearest;

        List<Vector2> platforms = new List<Vector2>(platformPositions);

        // Filter: remove last and current platform
        platforms.RemoveAll(p => p == lastPlatform);
        platforms.RemoveAll(p => p == currentPlatform);

        // Filter: remove all that are not in facing direction (if any)
        var availableOnSide = platforms.FindAll
        (
            p => facingDirection == LEFT? 
                p.x < GetPosition().x:
                p.x > GetPosition().x);
        if (availableOnSide != null && availableOnSide.Count > 0)
        {
            platforms.RemoveAll(p => !availableOnSide.Contains(p));
        }
        
        float shortestDistance = 10f;//Vector2.Distance(GetPosition(), platformPositions[0].position);
        
        if (platforms.Count == 0)
        {
            platforms = platformPositions;
        }

        nearest = platforms[0];


        foreach (var platformPos in platforms)
        {   
            var curDistance = Vector2.Distance(GetPosition(), platformPos);
            if (curDistance < shortestDistance && !( 
                    (platformPos.x < GetPosition().x && player.GetPosition().x < GetPosition().x) &&
                    (platformPos.x > GetPosition().x && player.GetPosition().x > GetPosition().x) ) )
            {
                shortestDistance = curDistance;
                nearest = platformPos;
                
                lastPlatform = new Vector2(currentPlatform.x, currentPlatform.y);
                currentPlatform = nearest;
                break;
            }
        }
        return nearest;
    }


    void inputs_Interact()
    {

        if (!justInteracted)
        {
            if (distanceToPlayer <= interactionRadius)
            {
                spriteRenderer.color = colorWhenInteractuable;
                OnInteraction();
            }
            else
            {
                spriteRenderer.color = defaultColor;
            }
        }
    }

    private void OnInteraction()
    {
        blinkingSprite.enabled = true;
        spriteRenderer.color = Color.white;
        interactions++;
        justInteracted = true;
    }
}
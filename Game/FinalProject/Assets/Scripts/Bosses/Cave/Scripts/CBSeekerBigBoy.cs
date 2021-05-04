using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBSeekerBigBoy : Entity
{
    #region Physics
    [Header("Physic Params")]
    [SerializeField] private float speedMultiplier;
    private float speed;

    [SerializeField] private float pushForce;
    private Vector2 push;
    private bool inPush;
    private bool stopped;
    #endregion

    #region Collisions
    [Header("Collisions")]
    [SerializeField] private List<GameObject> walls;
    private bool hitWall;
    [SerializeField] private float waitTimeWhenCollide;
    private float currentWaitTime;

    [SerializeField] private float distanceFromPlayerToStop;
    #endregion
    private bool chasingInGround;

    private Vector2 lastPlayerPosition;

    

    // Might inherit later *-*-*-*-*-*
    private PlayerManager player;
    private EnemyCollisionHandler eCollisionHandler;

    void Awake()
    {
        eCollisionHandler = (EnemyCollisionHandler) collisionHandler;
        eCollisionHandler.TouchingGroundHandler += eCollisionHandler_TouchingGround;
        eCollisionHandler.EnterTouchingContactHandler += eCollisionHandler_EnterCollision;

        speed = averageSpeed * speedMultiplier;
    }

    new void Start()
    {
        player = PlayerManager.instance;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
       
        if (hitWall)
        {
             
            if (currentWaitTime > waitTimeWhenCollide)
            {
                hitWall = false;
                currentWaitTime = 0;
            }
            else
            {
                currentWaitTime += Time.deltaTime;
                return;
            }
        }

        if (!isGrounded)
        {
            inPush = false;
        }

        if (isGrounded && !inPush)
        {
            lastPlayerPosition = player.GetPosition();

            if (player.isGrounded)
            {
                Vector2 direction = ((Vector3)lastPlayerPosition - GetPosition()).normalized;
                push = new Vector2(direction.x * pushForce * speed, 0);
                inPush = true;
            }
        }
        

        base.Update();
    }

    
    void FixedUpdate()
    {
        if (inPush)
        {
            rigidbody2d.AddForce(push * Time.deltaTime, ForceMode2D.Impulse);

            Vector2 vel = transform.rotation * rigidbody2d.velocity;
            if (vel.x > 0)
            {
                if (GetPosition().x > player.GetPosition().x && DistancedFromPlayer())
                {
                    StopMoving();
                }
            }
            else
            {
                if (GetPosition().x < player.GetPosition().x && DistancedFromPlayer())
                {
                    StopMoving();
                }
            }
            // jump
        }
        else
        {
        }
    }

    bool DistancedFromPlayer()
    {
        return Vector2.Distance(GetPosition(), player.GetPosition()) > distanceFromPlayerToStop;
    }

    void StopMoving()
    {
        hitWall = true;
        inPush = false;
        rigidbody2d.velocity = new Vector2();
    }

    public void ProjectileAttack()
    {
        //throw new System.NotImplementedException();
    }


    void eCollisionHandler_TouchingGround()
    {

    }

    void eCollisionHandler_EnterCollision(GameObject contact)
    {

        // Stops the enemy if touches any of the walls
        if (walls.Contains(contact))
        {
            StopMoving();
        }
    }
    
}

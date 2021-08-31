using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFox : Enemy
{
    private Inventory inventory;
    private Item stolenItem;
    private bool hasItem;
    [SerializeField] private GameObject leaveItem;
    [SerializeField] private Transform leaveItemPosition;
    [SerializeField] private float afterStealSpeed;
    [SerializeField] private float startEscapeTime;
    [SerializeField] GameObject stolenItemIcon;

    private float escapeTime;

    new void Start()
    {
        inventory = Inventory.instance;
        afterStealSpeed *= averageSpeed;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        hasItem = stolenItem != null;
        base.Update();
    }
    
    
    new void FixedUpdate()
    {
        if (hasItem)
        {
            if (fieldOfView.canSeePlayer)
            {
                enemyMovement.StopMovement();
                ChangeFacingDirection();
            }
            
            if (escapeTime < startEscapeTime)
            {
                if (fieldOfView.inFrontOfObstacle || groundChecker.isNearEdge)
                {
                    enemyMovement.Jump();
                }
                else
                {
                    enemyMovement.Translate(facingDirection == RIGHT? Vector2.right : Vector2.left, afterStealSpeed);
                }
                escapeTime += Time.deltaTime;
            }
            else
            {
                DestroyEntity();
            }
            stolenItemIcon.GetComponent<SpriteRenderer>().sprite = stolenItem.icon;
        }
        base.FixedUpdate();
    }

    protected override void MainRoutine()
    {
        if (!hasItem)
        {
            enemyMovement.DefaultPatrol();
        }
    }

    protected override void ChasePlayer()
    {
        if (!hasItem)
        {
            enemyMovement.GoToInGround(player.GetPosition(), chasing: true, checkNearEdge: false);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if (!hasItem)
        {
            Item item = inventory.GetRandomEdibleItem();
            if (item != null)
            {
                stolenItem = item;
                escapeTime = 0;
                inventory.Remove(stolenItem);
            }
        }
    }

    public override void ConsumeItem(Item item)
    {
        if (hasItem)
        {
            DropItem(stolenItem, leaveItemPosition.position);
        }
        stolenItem = item;
        escapeTime = 0;
    }

    private void DropItem(Item item, Vector3 position)
    {
        leaveItem.GetComponent<Inter>().SetItem(item);
        Instantiate(leaveItem, position, Quaternion.identity);
    }
}
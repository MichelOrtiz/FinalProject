using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFox : DogFox
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
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        hasItem = stolenItem != null;
        if (touchingPlayer)
        {
            ChangeFacingDirection();
        }
        if (InFrontOfObstacle())
        {
            //rigidbody2d.position = Vector3.MoveTowards(GetPosition(), new Vector3(GetPosition().x, jumpForce), chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
            //rigidbody2d.AddForce(Vector3.up * jumpForce * Time.deltaTime * rigidbody2d.gravityScale, ForceMode2D.Force);
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x * afterStealSpeed * Time.deltaTime, jumpForce * rigidbody2d.gravityScale);

        }
        if (hasItem)
        {
            if (CanSeePlayer())
            {
                ChangeFacingDirection();
            }
            if (escapeTime < startEscapeTime)
            {
                transform.Translate(Vector3.right * Time.deltaTime * afterStealSpeed);
                escapeTime += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
            stolenItemIcon.GetComponent<SpriteRenderer>().sprite = stolenItem.icon;
            // Move the opposite direction of the player
        }
        base.Update();
    }
    
    
    new void FixedUpdate()
    {
        
        base.FixedUpdate();
    }

    protected override void MainRoutine()
    {
        base.MainRoutine();
    }

    protected override void ChasePlayer()
    {
        if (!hasItem)
        {
            if (!touchingPlayer)
            {
                rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);
            }
        }
        /*else
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), afterStealVelocity * Time.deltaTime);
            ChangeFacingDirection();
        }*/
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
        Item item = inventory.GetRandomEdibleItem();
        
        if (hasItem)
        {
            ConsumeItem(item);
        }
        else
        {
            stolenItem = item;
            escapeTime = 0;
        }
        //stolenItem.RemoveFromInventory();
        inventory.Remove(stolenItem);
        
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

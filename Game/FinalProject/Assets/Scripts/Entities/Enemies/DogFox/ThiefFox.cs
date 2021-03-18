using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFox : DogFox
{

    private Inventory inventory;
    private Item stolenItem;
    private bool hasItem;
    [SerializeField] private Transform leaveItemPosition;
    [SerializeField] private float afterStealVelocity;
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
        if (hasItem)
        {
            if (escapeTime < startEscapeTime)
            {
                rigidbody2d.velocity = player.GetPosition().normalized * afterStealVelocity * Time.deltaTime;
                escapeTime += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
            stolenItemIcon.GetComponent<SpriteRenderer>().sprite = stolenItem.icon;
            // Move the opposite direction of the player
        }
        else
        {
            escapeTime = startEscapeTime;
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        base.MainRoutine();
    }

    protected override void ChasePlayer()
    {
        if (!hasItem)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);
            if (InFrontOfObstacle())
            {
                rigidbody2d.position = rigidbody2d.position = Vector3.MoveTowards(GetPosition(), new Vector3(GetPosition().x, jumpForce), chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
            }
        }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
        Item droppedItem = inventory.GetRandomEdibleItem();

        //DropItem(droppedItem, new Vector3(GetPosition().x, GetPosition().y + 1f));
        //Instantiate(droppedItem, new Vector3(GetPosition().x, GetPosition().y + 1f), Quaternion.identity);
        //droppedItem.RemoveFromInventory();
        stolenItem = droppedItem;
        
        inventory.Remove(droppedItem);
    }

    public override void ConsumeItem(Item item)
    {
        if (hasItem)
        {
            //DropItem(stolenItem, leaveItemPosition.position);
        }
        stolenItem = item;
    }

    /*private void DropItem(Item item, Vector3 position)
    {
        GameObject objProjectile = Instantiate(objPrefab, player.GetPosition(), transform.rotation);
        objProjectile.GetComponent<ObjProjectile>().SetItem(item);
        objProjectile.GetComponent<Rigidbody2D>().position = Vector3.MoveTowards(objProjectile.transform.position, position, 10 * Time.deltaTime);
    }*/
}

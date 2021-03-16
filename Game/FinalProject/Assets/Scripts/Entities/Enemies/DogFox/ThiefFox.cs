using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFox : DogFox
{

    private Inventory inventory;
    private Item stolenItem;
    [SerializeField] Transform leaveItemPosition;
    [SerializeField] float afterStealVelocity;

    new void Start()
    {
        inventory = Inventory.instance;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void ChasePlayer()
    {
        rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);
        if (InFrontOfObstacle())
        {
            rigidbody2d.position = rigidbody2d.position = Vector3.MoveTowards(GetPosition(), new Vector3(GetPosition().x, jumpForce), chaseSpeed * Time.deltaTime * rigidbody2d.gravityScale);
        }
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
        if (stolenItem != null)
        {
            Instantiate(stolenItem, leaveItemPosition.position, Quaternion.identity);
        }
        stolenItem = inventory.GetRandomEdibleItem();
        inventory.Remove(stolenItem);
        rigidbody2d.velocity = player.GetPosition().normalized * afterStealVelocity * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}

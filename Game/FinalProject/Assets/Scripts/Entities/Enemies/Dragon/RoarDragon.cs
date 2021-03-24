using UnityEngine;
using System.Collections.Generic;
public class RoarDragon : Dragon
{
    //[SerializeField] private float roarAffectDistance;
    [SerializeField] private float enhanceMultiplier;
    [SerializeField] private float baseEnhancementTime;
    private float enhancementTime;
    private bool roared;
    private bool isRoaring;
    private bool canFallToSurface;
    private List<Dragon> dragons;
    new void Start()
    {
        dragons = ScenesManagers.GetObjectsOfType<Dragon>();
        base.Start();
    }

    new void Update()
    {
        if (roared)
        {
            if (enhancementTime < baseEnhancementTime)
            {
                enhancementTime += Time.deltaTime;
            }
            else
            {
                foreach (var dragon in dragons)
                {
                    dragon.NerfValues(enhanceMultiplier);
                }
                roared = false;
                enhancementTime = 0;
            }
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        if (!roared)
        {
            if (InFrontOfObstacle() || (IsNearEdge() && !CanFallToSurface()))
            {
                if (waitTime > 0)
                {
                    isWalking = false;
                    waitTime -= Time.deltaTime;
                    return;
                }
                ChangeFacingDirection();
                waitTime = startWaitTime;
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * normalSpeed);
                isWalking = true;
            }
        }
    }

    protected override void ChasePlayer()
    {
        isWalking = false;
        if (facingDirection == LEFT && player.GetPosition().x > this.GetPosition().x || facingDirection == RIGHT && player.GetPosition().x < this.GetPosition().x)
        {
            ChangeFacingDirection();
        }
        if (!roared)
        {
            //animator.SetBool("Is Roaring", true);
            animator.SetTrigger("Roar");
            roared = true;
            foreach (var dragon in dragons)
            {
                dragon.EnhanceValues(enhanceMultiplier);
            }
        }
    }

    private bool CanFallToSurface()
    {
        return (Physics2D.Raycast(groundCheck.position, Vector3.down, -downDistanceGroundCheck)).collider;
    }
}
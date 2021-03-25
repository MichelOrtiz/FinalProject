using UnityEngine;
public class SwordGriffin : Griffin
{
    [SerializeField] private float secondFovDistance;
    [SerializeField] private float firstFovSpeedMultiplier;
    [SerializeField] private float secondFovSpeedMultiplier;
    [SerializeField] private float baseWaitTimeAfterDestroy;
    [SerializeField] protected bool touchingBreakableObject;
    private float firstFovSpeed;
    private float secondFovSpeed;
    private float waitTimeAfterDestroy;
    private bool destroyedObject;
    private GameObject breakableObject;
    new void Start()
    {
        firstFovSpeed = averageSpeed * firstFovSpeedMultiplier;
        secondFovSpeed = averageSpeed * secondFovSpeedMultiplier;
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    protected override void MainRoutine()
    {
        return;
    }

    protected override void ChasePlayer()
    {
        if (!destroyedObject)
        {
            if (facingDirection == LEFT && player.GetPosition().x > this.GetPosition().x || facingDirection == RIGHT && player.GetPosition().x < this.GetPosition().x)
            {
                ChangeFacingDirection();
            }
            // first fov
            if (!CanSeePlayerSecondFov())
            {
                Debug.Log("should move");
                MoveTowardsPlayer(firstFovSpeed);
            }
            else
            {
                MoveTowardsPlayer(secondFovSpeed);
            }
            Debug.Log("should move");
            //rigidbody2d.velocity = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);
            if (touchingBreakableObject)
            {
                Destroy(breakableObject);
                destroyedObject = true;
                // paralized 2 seg
            }
        }
        else
        {
            if (waitTimeAfterDestroy < baseWaitTimeAfterDestroy)
            {
                waitTimeAfterDestroy += Time.deltaTime;
            }
            else
            {
                destroyedObject = false;
                waitTimeAfterDestroy = 0;
            }
        }
        
    }

    private void MoveTowardsPlayer(float speed)
    {
        Vector3 playerPosition = (player.isGrounded? player.GetPosition(): new Vector3(player.GetPosition().x, GetPosition().y));
        if (!InFrontOfObstacle() && isGrounded && !touchingPlayer)
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), playerPosition, speed * Time.deltaTime);
        }
        //rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), speed * Time.deltaTime);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (other.gameObject.tag == "Breakable")
        {
            touchingBreakableObject = true;
            breakableObject = other.gameObject;
        }
    }

    protected override void OnCollisionExit2D(Collision2D other)
    {
        base.OnCollisionExit2D(other);
        if (other.gameObject.tag == "Breakable")
        {
            touchingBreakableObject = false;
        }
    }

    private bool CanSeePlayerSecondFov()
    {
        Debug.Log(GetDistanceFromPlayerFov());
        return GetDistanceFromPlayerFov() <= secondFovDistance;
    }
}
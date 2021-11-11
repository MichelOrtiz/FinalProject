using UnityEngine;
public class Arandana : Enemy
{
    private CollisionHandler groundHandler;
    float rotation;
    [SerializeField] private KnockbackState onRotateForward;
    new void Start()
    {
        base.Start();
        groundHandler = groundChecker.GetComponent<CollisionHandler>();
    }


    new void Update()
    {
        base.Update();
        animationManager.ChangeAnimation("walk");
        /*var eulerAngles = transform.localEulerAngles;
        rotation = Mathf.RoundToInt(eulerAngles.z);*/
        if (fieldOfView.inFrontOfObstacle)
        {   
            //enemyMovement.StopMovement();

            ChangeFacingDirection();
        }
        else if (groundChecker.isNearEdge && (!groundChecker.isGrounded && groundHandler.Contacts.Exists(c => GroundChecker.GroundTags.Exists(tg => tg == c.tag ))))
        {
            enemyMovement.StopAllMovement();

            //ebug.DrawLine(obstacleCheck.position, checkDir);

            /*if (!fieldOfView.RayHitObstacle(obstacleCheck.position, checkDir))
            {*/
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,  transform.eulerAngles.z - 90 );

            //}
            //HandleForward();
        }
    }

    void HandleForward()
    {
        var forward = Instantiate(onRotateForward);
        var rotation = Mathf.RoundToInt(transform.eulerAngles.z);
        if (rotation == 90 || rotation == 270)
        {
            forward.angle = 180;
        }
        statesManager.AddState(forward);
    }

    new void FixedUpdate()
    {
        

        if (touchingPlayer)
        {
            enemyMovement.StopAllMovement();
        }
        base.FixedUpdate();
    }

    protected override void MainRoutine()
    {
        if (groundChecker.isGrounded && !fieldOfView.inFrontOfObstacle && !touchingPlayer)
        {
            enemyMovement.Translate(transform.right, chasing: false);
            animationManager.ChangeAnimation("walk");
        }
    }

    protected override void ChasePlayer()
    {
        if (groundChecker.isGrounded && !fieldOfView.inFrontOfObstacle && !touchingPlayer)
        {
            // player has to be in the same ground to use chasing speed
            enemyMovement.Translate(transform.right, chasing: player.groundChecker.lastGroundTag == groundChecker.lastGroundTag);
            animationManager.ChangeAnimation("walk", enemyMovement.ChaseSpeed * 1 / enemyMovement.DefaultSpeed);
        }
    }
}
using UnityEngine;
public class PatrollGriffin : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float patrolDistance;
    [SerializeField] private Material meshRenderDefault;
    [SerializeField] private Material meshRenderSawPlayer;
    [SerializeField] private MeshFov meshFov;
    [SerializeField] private State meshEffectOnPlayer; 
    private Material currentMeshMaterial;
    [SerializeField]private bool reachedDestination;
    private bool canContinue;
    private Vector2 startPosition;
    private Vector2 patrolDestination;
    private float curWaitTime;
    private bool goingRight;

    new void Start()
    {
        base.Start();
        //fovOrigin.GetComponent<MeshFov>().Setup(fovAngle, viewDistance, meshRenderDefault, fieldOfView.FovType);
        fieldOfView.FovOrigin.GetComponent<MeshFov>().Setup(fieldOfView.FovAngle, fieldOfView.ViewDistance, meshRenderDefault, fieldOfView.FovType);
        currentMeshMaterial = meshRenderDefault;

        goingRight = facingDirection == RIGHT;
    }

    new void Update()
    {
        if (fieldOfView.canSeePlayer && currentMeshMaterial != meshRenderSawPlayer)
        {
            //fovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderSawPlayer;
            fieldOfView.FovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderSawPlayer;
            currentMeshMaterial = meshRenderSawPlayer;
        }
        else if(!fieldOfView.canSeePlayer && currentMeshMaterial != meshRenderDefault)
        {
            //fovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderDefault;
            fieldOfView.FovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderDefault;
            currentMeshMaterial= meshRenderDefault;
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        //base.MainRoutine();
        
        if ( (!reachedDestination ) && groundChecker.isGrounded && !groundChecker.isNearEdge && !fieldOfView.inFrontOfObstacle)
        {
            animationManager.ChangeAnimation("walk");
            enemyMovement.GoToInGround(patrolDestination, chasing: false, checkNearEdge: false);
            reachedDestination = MathUtils.GetAbsXDistance(GetPosition(), patrolDestination) < 0.5f;
        }
        else
        {
            enemyMovement.StopMovement();
            reachedDestination = true;
            if (curWaitTime > enemyMovement.WaitTime)
            {
                startPosition = GetPosition();
                goingRight = !goingRight;

                enemyMovement.ChangeFacingDirection();

                UpdatePatrolDestination();

                reachedDestination = false;
                curWaitTime = 0;
            }
            else
            {
                curWaitTime += Time.deltaTime;
            }
        }
    }

    protected override void ChasePlayer()
    {
        player.statesManager.AddStateDontRepeat(meshEffectOnPlayer);
    }

    protected override void groundChecker_Grounded(string groundTag)
    {
        startPosition = GetPosition();
        UpdatePatrolDestination();
    }

    void UpdatePatrolDestination()
    {
        patrolDestination = new Vector2(startPosition.x + (goingRight ? patrolDistance : -patrolDistance), startPosition.y);
    }
}
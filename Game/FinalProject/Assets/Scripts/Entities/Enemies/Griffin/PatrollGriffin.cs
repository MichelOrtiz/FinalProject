using UnityEngine;
public class PatrollGriffin : Griffin
{
    [SerializeField] private float patrolDistance;
    [SerializeField] private Material meshRenderDefault;
    [SerializeField] private Material meshRenderSawPlayer;
    [SerializeField] private MeshFov meshFov;
    private Material currentMeshMaterial;
    private bool reachedDestinationPatrol;
    private Vector2 destinationPatrol;
    new void Start()
    {
        base.Start();
        //fovOrigin.GetComponent<MeshFov>().Setup(fovAngle, viewDistance, meshRenderDefault, fieldOfView.FovType);
        fieldOfView.FovOrigin.GetComponent<MeshFov>().Setup(fieldOfView.FovAngle, fieldOfView.ViewDistance, meshRenderDefault, fieldOfView.FovType);

        currentMeshMaterial = meshRenderDefault;
    }

    new void Update()
    {
        if (CanSeePlayer() && currentMeshMaterial != meshRenderSawPlayer)
        {
            //fovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderSawPlayer;
            fieldOfView.FovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderSawPlayer;
            currentMeshMaterial = meshRenderSawPlayer;
        }
        else if(!CanSeePlayer() && currentMeshMaterial != meshRenderDefault)
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
        if (isGrounded && !InFrontOfObstacle() && !IsNearEdge())
        {
            if (waitTime > startWaitTime)
            {
                ChangeFacingDirection();
                reachedDestinationPatrol = true;
                destinationPatrol = new Vector2(GetPosition().x +(facingDirection == RIGHT?  -patrolDistance : +patrolDistance), GetPosition().y);
                waitTime = 0;
            }
            else
            {
                reachedDestinationPatrol = false;
                rigidbody2d.position = Vector3.MoveTowards(GetPosition(), destinationPatrol, normalSpeed * Time.deltaTime);
                waitTime += Time.deltaTime;
            }
        }
        
    }

    protected override void ChasePlayer()
    {
        player.statesManager.AddState(atackEffect);
    }
    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }
}
using UnityEngine;
public class PatrollGriffin : Griffin
{
    [SerializeField] private float patrolDistance;
    [SerializeField] private Material meshRenderDefault;
    [SerializeField] private Material meshRenderSawPlayer;
    [SerializeField] private MeshFov meshFov;
    private Material currentMeshMaterial;
    //private bool reachedDestination;
    new void Start()
    {
        base.Start();
        /*fovOrigin.GetComponent<MeshFov>().ViewDistance = viewDistance;
        fovOrigin.GetComponent<MeshFov>().FovAngle = fovAngle;
        fovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderDefault;
        fovOrigin.GetComponent<MeshFov>().AngleFromPlayer = GetAngleFromPlayer();*/
       //fovOrigin.GetComponent<MeshFov>().Origin = fovOrigin.position.normalized;
        fovOrigin.GetComponent<MeshFov>().Setup(fovAngle, viewDistance, meshRenderDefault, fovType);

        //MeshFov mesh = fovOrigin.GetComponent<MeshFov>().;
        //mesh = new MeshFov(fovAngle, viewDistance, meshRenderDefault);
        //meshFov = mesh;//new MeshFov(fovAngle, viewDistance, meshRenderDefault);
        currentMeshMaterial = meshRenderDefault;
    }

    new void Update()
    {
        //fovOrigin.GetComponent<MeshFov>().AngleFromPlayer = GetAngleFromPlayer();

        if (CanSeePlayer() && currentMeshMaterial != meshRenderSawPlayer)
        {
            fovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderSawPlayer;
            currentMeshMaterial = meshRenderSawPlayer;
        }
        else if(!CanSeePlayer() && currentMeshMaterial != meshRenderDefault)
        {
            fovOrigin.GetComponent<MeshFov>().MeshMaterial = meshRenderDefault;
            currentMeshMaterial= meshRenderDefault;
        }
        base.Update();
    }

    protected override void MainRoutine()
    {
        /*if (isGrounded && !InFrontOfObstacle() && !IsNearEdge())
        {
            rigidbody2d.position = Vector3.MoveTowards(GetPosition(), Vector2.right * patrolDistance, normalSpeed * Time.deltaTime);
        }*/
        base.MainRoutine();
    }

    protected override void ChasePlayer()
    {
        // capture player
    }
    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }
}
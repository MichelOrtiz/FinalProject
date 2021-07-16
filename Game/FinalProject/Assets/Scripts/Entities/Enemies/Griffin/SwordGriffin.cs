using UnityEngine;
public class SwordGriffin : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float secondFovDistance;
    [SerializeField] private float firstFovSpeedMultiplier;
    private float firstFovSpeed;
    [SerializeField] private float secondFovSpeedMultiplier;
    private float secondFovSpeed;
    [SerializeField] private float baseWaitTimeAfterDestroy;
    private float waitTimeAfterDestroy;
    [SerializeField] private State effectOnDestroyObject;
    private bool destroyedObject;

    // private GameObject breakableObject;


    new void Start()
    {
        firstFovSpeed = averageSpeed * firstFovSpeedMultiplier;
        secondFovSpeed = averageSpeed * secondFovSpeedMultiplier;
        
        base.Start();
    }

    protected override void ChasePlayer()
    {
        float speed = fieldOfView.GetDistanceFromPlayerFov() >= secondFovDistance ? firstFovSpeed : secondFovSpeed;
        
        if (MathUtils.GetAbsXDistance(player.GetPosition(), GetPosition()) > 2f)
        {
            enemyMovement.GoToInGround(player.GetPosition(), speed, checkNearEdge: true);
        }
    }

    protected override void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact.tag == "Breakable")
        {
            //breakableObject = contact;
            Destroy(contact);
            //destroyedObject = true;
            if (!touchingPlayer)
            {
                statesManager.AddState(effectOnDestroyObject);
            }
        }
    }
}
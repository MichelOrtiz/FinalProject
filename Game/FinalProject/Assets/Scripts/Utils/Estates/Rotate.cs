using UnityEngine;

[CreateAssetMenu(fileName="New Rotate", menuName = "States/new Rotate")]
public class Rotate : State
{
    enum Direction { Clockwise, Counterclockwise }
    [SerializeField] private Direction direction;
    private Quaternion initialRotation;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        manager.hostEntity.rigidbody2d.gravityScale = 0;
        manager.hostEntity.groundChecker.GetComponent<Collider2D>().isTrigger = true;
        manager.hostEntity.physics.SetKinematic(duration + 0.1f);

        initialRotation = manager.hostEntity.transform.rotation;
    }

    public override void Affect()
    {
        if (currentTime > duration)
        {
            currentTime = 0;
            StopAffect();
        }
        else
        {
            manager.hostEntity.transform.Rotate(direction == Direction.Clockwise? -Vector3.forward : Vector3.forward, 360 * Time.deltaTime / duration);
            currentTime += Time.deltaTime;
        }
    }

    public override void StopAffect()
    {
        manager.hostEntity.groundChecker.GetComponent<Collider2D>().isTrigger = false;

        manager.hostEntity.transform.rotation = initialRotation;

        manager.hostEntity.rigidbody2d.velocity = Vector3.zero;
        manager.hostEntity.rigidbody2d.angularVelocity = 0;
        manager.hostEntity.rigidbody2d.gravityScale = 1;
        base.StopAffect();
    }
}
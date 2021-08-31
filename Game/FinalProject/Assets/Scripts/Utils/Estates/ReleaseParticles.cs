using UnityEngine;
[CreateAssetMenu(fileName="New ReleaseParticles", menuName = "States/new ReleaseParticles")]
public class ReleaseParticles : State
{
    [SerializeField] private GameObject particles;
    [SerializeField] private bool stopMovement;
    
    private float prevGravity;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        
        try
        {
            // Requires the entity to have a projectile shooter
            Vector2 shotPos = manager.hostEntity.GetComponentInChildren<ProjectileShooter>().ShotPos.position;
            Instantiate(particles, shotPos, manager.hostEntity.transform.rotation);
        }
        catch (System.NullReferenceException)
        {
            Instantiate(particles, manager.hostEntity.transform.position, manager.hostEntity.transform.rotation);
        }

        if (stopMovement)
        {
            manager.hostEntity.enabled = false;
            //manager.hostEntity.rigidbody2d.isKinematic = true;

           // prevGravity = manager.hostEntity.rigidbody2d.gravityScale;
            //manager.hostEntity.rigidbody2d.gravityScale = 0;
        }
        
    }

    public override void Affect()
    {
       if (currentTime > duration)
       {
           StopAffect();
       }
       else
       {
           currentTime += Time.deltaTime;
       }
    }

    public override void StopAffect()
    {
        particles.GetComponent<ParticleSystem>().Stop();
        //Destroy(particles);

        if (stopMovement)
        {
            manager.hostEntity.enabled = true;
            manager.hostEntity.rigidbody2d.isKinematic = false;
            //manager.hostEntity.rigidbody2d.gravityScale = prevGravity;
        }
        base.StopAffect();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VientoHazard : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float damageThreshold = defaultDamageThreshold;
    public const float defaultDamageThreshold = 0; 
    [SerializeField] private State hazardState;
    private PlayerManager player;
    new private ParticleSystem particleSystem;
    void Start()
    { 
        player = PlayerManager.instance;
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem.isStopped)
        {
            particleSystem.Play();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player" && !player.isImmune )
        {
            player.TakeTirement(damageAmount);
            if (damageAmount > damageThreshold)
            {
                player.SetImmune();
            }
            player.statesManager.AddState(hazardState);
            ParticleSystem.CollisionModule collisionModule = particleSystem.collision;
            if(player.statesManager.currentStates.Contains(hazardState)){
                collisionModule.colliderForce = 3000f;
            }else{
                collisionModule.colliderForce = 0f;
            }
        }
    }
}

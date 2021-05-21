using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private State effectOnPlayer;
    [SerializeField] private bool stopEmittingWhenParticleCollide;
    public delegate void ParticleCollision(GameObject other);
    public event ParticleCollision ParticleCollisionHandler;
    protected virtual void InParticleCollsion(GameObject other){
        ParticleCollisionHandler?.Invoke(other);
    }
    private PlayerManager player;
    new private ParticleSystem particleSystem; 
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem.isStopped)
        {
            particleSystem.Play();
        }
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnParticleCollision is called when a particle hits a collider.
    /// </summary>
    /// <param name="other">The GameObject hit by the particle.</param>
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.TakeTirement(damageAmount);
            if (!player.statesManager.currentStates.Contains(effectOnPlayer))
            {
                player.statesManager.AddState(effectOnPlayer);
            }
        }

        if (stopEmittingWhenParticleCollide)
        {
            particleSystem.Stop();
        }
        InParticleCollsion(other);
    }
    
}

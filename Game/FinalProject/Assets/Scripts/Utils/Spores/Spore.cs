using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private State effectOnPlayer;
    [SerializeField] private bool stopEmittingWhenParticleCollide;

    #region Events
    public delegate void ParticleCollision(GameObject other);
    public event ParticleCollision ParticleCollisionHandler;
    protected virtual void InParticleCollsion(GameObject other){
        ParticleCollisionHandler?.Invoke(other);
    }

    private bool onEndedCalled;
    public delegate void EmmissionEnded(Spore sender);
    public event EmmissionEnded EmmissionEndedHandler;
    protected virtual void OnEmmissionEnded(Spore sender)
    {
        onEndedCalled = true;
        EmmissionEndedHandler?.Invoke(sender);
    }

    #endregion
    private PlayerManager player;
    new private ParticleSystem particleSystem;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        onEndedCalled = false;
    }

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
        if (particleSystem.isStopped && !onEndedCalled)
        {
            OnEmmissionEnded(this);
        }
    }


    /// <summary>
    /// OnParticleCollision is called when a particle hits a collider.
    /// </summary>
    /// <param name="other">The GameObject hit by the particle.</param>
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player" && !player.isImmune )
        {
            player.TakeTirement(damageAmount);
            if (damageAmount > 0)
            {
                player.SetImmune();
            }
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
    public void OnParticleSystemStopped()
    {
        OnEmmissionEnded(this);
    }
}

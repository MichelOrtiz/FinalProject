using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalAllergyHazard : MonoBehaviour
{
    [SerializeField] private float sneezeProbability;
    [SerializeField] private State sneezeState;
    [SerializeField] private float interval;

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float particlesTime;

    void Start()
    {
        InvokeRepeating("HandleSneeze", interval, interval);
    }
    void HandleSneeze()
    {
        if (RandomGenerator.MatchProbability(sneezeProbability))
        {
            transform.position = PlayerManager.instance.transform.position;
            particles.Play();
            PlayerManager.instance.statesManager.AddState(sneezeState);
        }
        Invoke("StopParticles", particlesTime);
    }


    void StopParticles()
    {
        particles.Stop();
    }
}

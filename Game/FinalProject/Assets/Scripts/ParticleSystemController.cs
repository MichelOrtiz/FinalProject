using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public static ParticleSystemController instance;
    public ParticleSystem bParticleSystem;
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        bParticleSystem.Stop();
    }
    public void Play(){
        transform.position = PlayerMovement.player.transform.position;
        bParticleSystem.Play();
    }
}

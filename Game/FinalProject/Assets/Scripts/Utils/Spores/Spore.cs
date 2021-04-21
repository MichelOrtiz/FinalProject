using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private State effectOnPlayer;
    private PlayerManager player; 
    void Start()
    {
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
        //Debug.Log("Collision" + other.gameObject);
        if (other.gameObject.tag == "Player")
        {
            player.TakeTirement(damageAmount);
            if (!player.statesManager.currentStates.Contains(effectOnPlayer))
            {
                player.statesManager.AddState(effectOnPlayer);
            }
        }
    }

    
}

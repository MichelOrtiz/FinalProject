using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : MonoBehaviour
{
    public float damageAmount;
    public float normalSpeed;
    public float chaseSpeed;
    public int fieldView;
    public float spawnRate;
    public ushort spawnQuantity;

    public Animator animator;

    public Transform collisionChecker;
    protected RaycastHit2D collissionInfo;
    protected new Rigidbody2D rigidbody2D;
    public float checkRadius;

    public bool inFrontOfObstacle;

    public delegate void EffectOnPlayer();
    public static event EffectOnPlayer EffectOnPlayerEvent;
    
    

    public abstract IEnumerator MainRoutine();
    public void Start()
    {
        //EffectOnPlayerEvent = new EffectOnPlayer(ActionOnPlayer)
    }

    public void ActionOnPlayer()
    {
        EffectOnPlayerEvent.Invoke(); 
    }


    /// <summary>
    /// Updates the state of every animation needed
    /// </summary>
    protected abstract void UpdateAnimationState();
}
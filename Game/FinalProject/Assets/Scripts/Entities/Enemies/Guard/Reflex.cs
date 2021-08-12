using UnityEngine;
using System.Collections;
public class Reflex : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float delay;
    private float curTime;

    #region CloningStuff
    private Queue cloners;
    private Cloner currentCloner;
    class Cloner
    {
        public Vector3 position;
        public Quaternion rotation;
        public AnimationState animation;
    }
    enum AnimationState
    {
        Idle, 
        Walk, 
        Jump, 
        Run, 
        Fall,
        Fly
    }
    #endregion

    new void Awake()
    {
        base.Awake();
        cloners = new Queue();
    }
    
    new void FixedUpdate()
    {
        // Each frame, a Cloner is added to the Queue, storing position, rotation and animation state from player current state
        cloners.Enqueue( new Cloner { position = player.GetPosition(), rotation = player.transform.rotation, animation = GetAnimationState() }  );
        if (curTime > delay)
        {
            // Takes a Cloner aut of the Queue, and assigns the info to the self variables
            currentCloner = (Cloner)cloners.Dequeue();
            transform.position = currentCloner.position;
            transform.rotation = currentCloner.rotation;
            SetAnimation(currentCloner.animation);
        }
        else
        {
            curTime += Time.deltaTime;
        }
        base.FixedUpdate();
    }

    /// <summary>
    /// Gets the player current animation state
    /// </summary>
    /// <returns></returns>
    AnimationState GetAnimationState()
    {
        if (player.isWalking)
        {
            return AnimationState.Walk;
        }
        else if (player.isJumping)
        {
            return AnimationState.Jump;
        }
        else if (player.isFalling)
        {
            return AnimationState.Fall;
        }
        else if (player.isRunning)
        {
            return AnimationState.Run;
        }
        else if (player.isFlying)
        {
            return AnimationState.Fly;
        }
        else
        {
            return AnimationState.Idle;
        }
    }

    /// <summary>
    /// Updates current state so the upper (Entity) class does the animations
    /// </summary>
    /// <param name="animationState"></param>
    void SetAnimation(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                isWalking = false;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                break;
            case AnimationState.Walk:
                isWalking = true;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                isFlying = false;
                break;
            case AnimationState.Jump:
                isWalking = false;
                isJumping = true;
                isRunning = false;
                isFalling = false;
                isFlying = false;
                break;
            case AnimationState.Run:
                isWalking = true;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                isFlying = false;
                break;
            case AnimationState.Fall:
                isWalking = false;
                isJumping = false;
                isRunning = false;
                isFalling = true;
                isFlying = false;
                break;
            case AnimationState.Fly:
                isWalking = false;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                isFlying = true;
                break;
        }
    }
}
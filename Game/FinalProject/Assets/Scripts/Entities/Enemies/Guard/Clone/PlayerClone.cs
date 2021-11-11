using UnityEngine;
using System.Collections;
public abstract class PlayerClone : Enemy
{
    [Header("Self Additions")]
    public float delay;
    private float curTime;

    #region CloningStuff
    protected Queue cloners;
    protected Cloner currentCloner;
    protected class Cloner
    {
        public Vector3 position;
        public Quaternion rotation;
        //public AnimationState animation;
        public string animationState;
    }
    /*protected enum AnimationState
    {
        Idle, 
        Walk, 
        Jump, 
        Run, 
        Fall,
        Fly, 
        Aim
    }*/
    #endregion

    protected abstract void CloneMovement();

    new void Awake()
    {
        base.Awake();
        cloners = new Queue();
        itemInteractionManager.entity = this;
    }

    new void Start()
    {
        base.Start();
    }
    
    new void FixedUpdate()
    {
        // Every frame, a Cloner is added to the Queue, storing position, rotation and animation state from player current state
        if (!player.collisionHandler.Contacts.Exists(c => c.tag == "SafeZone"))
        {
            cloners.Enqueue( new Cloner { position = player.GetPosition(), rotation = player.transform.rotation, animationState = GetAnimationState() }  );
            if (curTime > delay)
            {
                // Takes a Cloner out of the Queue, updating the currentCloner
                currentCloner = (Cloner)cloners.Dequeue();
                CloneMovement();
                SetAnimation(currentCloner.animationState);
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }
        base.FixedUpdate();
    }

    /// <summary>
    /// Gets the player current animation state
    /// </summary>
    /// <returns></returns>
    protected string GetAnimationState()
    {
        return player.animationManager.currentState;
    }

    /// <summary>
    /// Updates current state so the upper (Entity) class does the animations
    /// </summary>
    /// <param name="animationState"></param>
    protected void SetAnimation(string animation)
    {
        animationManager.ChangeAnimationNoFilter(animation);
    }
    
}
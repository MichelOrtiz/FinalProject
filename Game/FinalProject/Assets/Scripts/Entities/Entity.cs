using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomGenerator;
public class Entity : MonoBehaviour
{
    protected const string LEFT = "left";
    protected const string RIGHT = "right";

    #region Normal States
    public bool isWalking;
    public bool isGrounded;
    public bool isJumping;
    public bool isFlying;
    #endregion

    #region Special States
    public bool isParalized;
    public bool isCaptured;
    public bool isInFear;
    public bool isDizzy;
    public bool isBrainFrozen;
    public bool isResting;
    public bool isChasing;
    #endregion

    #region Physic Parameters
    public float walkingSpeed;
    public float jumpForce;
    public float jumpTime;
    #endregion
    
    #region Layers and rigids
    public Rigidbody2D rigidbody2d;
    public Animator animator;
    
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //UpdateAnimation();
    }

    /*public IEnumerator Paralized(int time)
    {
        yield return new WaitForSeconds(time);
    }*/
    
    /*IEnumerator Fear(int time)
    {
        ushort jumps = NewRandom(3, 10);
        for (int i = 0; i < jumps; i++)
        {
            rigidbody2d.velocity = new Vector2(NewRandom(3,10), jumpForce);
        }
    }
    IEnumerator Dizzy(int time);
    IEnumerator FrozenBragvin(int time);
    IEnumerator Rest(int time);
    IEnumerator Chase(int time);*/

    public void UpdateAnimation()
    {
        animator.SetBool("Is Walking", isWalking);
        animator.SetBool("Is Paralized", isParalized);
        animator.SetBool("Is Captured", isCaptured);
        animator.SetBool("Is In Fear", isInFear);
        animator.SetBool("Is Brain Frozen", isBrainFrozen);
        animator.SetBool("Is Resting", isResting);
        animator.SetBool("Is Chasing", isChasing);
    }
}
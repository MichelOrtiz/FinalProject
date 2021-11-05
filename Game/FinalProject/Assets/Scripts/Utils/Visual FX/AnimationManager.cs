using System;
using System.Collections;
using UnityEngine;
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Entity entity;
    public Entity Entity { get => entity; set => entity = value; }
    public Animator animator;
    public string previousState;
    public string currentState;
    public string nextState;
    public bool nextStateEnabled;


    void Update()
    {
        if (nextStateEnabled)
        {
            if (CurrentAnimationFinished())
            {
                ChangeAnimation(nextState);
                nextStateEnabled = false;
                nextState = "";
            }
        }
    }

    public void ChangeAnimation(string state)
    {
        animator.speed = 1;

        state = FilterEnemy(state);

        if (currentState == state) return;

        animator.Play(state);

        previousState = currentState;

        currentState = state;
    }

    public void ChangeAnimationNoFilter(string state)
    {
        animator.speed = 1;
        
        if (currentState == state && AnimatorIsPlaying()) return;

        animator.Play(state);

        previousState = currentState;

        currentState = state;
    }

    public void ChangeAnimation(string state, float speed)
    {
        ChangeAnimation(state);
        animator.speed = speed;
    }

    public void StopAnimation()
    {
        animator.StopPlayback();
        currentState = "";
        nextState = "";
    }

    public void RestartAnimation()
    {
        animator.Play(currentState, entity.GetComponentInChildren<SpriteRenderer>().gameObject.layer, 0f);

    }

    public void RestartAnimation(string state)
    {
        animator.Play(FilterEnemy(state), entity.GetComponentInChildren<SpriteRenderer>().gameObject.layer, 0f);
    }

    public void SetNextAnimation(string state)
    {
        nextState = state;
        nextStateEnabled = true;
    }

    public bool CurrentAnimationFinished()
    {
        //return GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        return !(AnimatorIsPlaying(currentState));
    }

    public bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool AnimatorIsPlaying(string animation)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }

    public string FilterEnemy(string state)
    {
        if (entity is Enemy)
        {
            var enemy = entity as Enemy;
            state = enemy.enemyName + "_" + state;
        }
        return state;
    }


    public void ChangeAnimationUntil(Func<bool> checkMethod, string nextAnimation) 
    {
        StartCoroutine(SetNextAnimUntil(checkMethod, nextAnimation));
    }

    public void SetCurrentState(string state, bool filterEnemy)
    {
        currentState = filterEnemy? FilterEnemy(state) : state;
    }


    IEnumerator SetNextAnimUntil(Func<bool> checkMethod, string nextAnimation)
    {
        yield return WaitUntilTrue(checkMethod);
        SetNextAnimation(nextAnimation);
    }

    IEnumerator WaitUntilTrue(Func<bool> checkMethod)
    {
        while (checkMethod() == false)
        {
            yield return null;
        }
    }
}
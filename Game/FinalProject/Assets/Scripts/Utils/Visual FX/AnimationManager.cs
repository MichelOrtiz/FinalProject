using UnityEngine;
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Entity entity;
    public Entity Entity { get => entity; set => entity = value; }
    [SerializeField] private Animator animator;
    public string previousState;
    public string currentState;
    public string nextState;
    public bool nextStateEnabled;


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
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
        
        if (currentState == state) return;

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
        animator.enabled = false;
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
        nextState = FilterEnemy(state);
        nextStateEnabled = true;
    }

    private bool CurrentAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    private string FilterEnemy(string state)
    {
        if (entity is Enemy)
        {
            var enemy = entity as Enemy;
            state = enemy.enemyName + "_" + state;
        }
        return state;
    }

}
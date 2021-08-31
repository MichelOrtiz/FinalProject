using UnityEngine;
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Entity entity;
    [SerializeField] private Animator animator;
    public string currentState;


    public void ChangeAnimation(string state)
    {
        animator.speed = 1;
        if (entity is Enemy)
        {
            var enemy = entity as Enemy;
            state = enemy.enemyName + "_" + state;
        }

        if (currentState == state) return;

        animator.Play(state);

        currentState = state;
    }

    public void ChangeAnimation(string state, float speed)
    {
        ChangeAnimation(state);
        animator.speed = speed;
    }

}
using UnityEngine;
[CreateAssetMenu(fileName = "New AnimationAndState", menuName = "States/new AnimationAndState")]
public class AnimationAndState : State
{
    [SerializeField] private string animationMessage;
    [SerializeField] private State stateAfter;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        manager.hostEntity.animator.SetBool(animationMessage, true);
    }
    public override void Affect()
    {
        if (currentTime > duration)
        {
            StopAffect();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    public override void StopAffect()
    {
        manager.hostEntity.animator.SetBool(animationMessage, false);
        manager.hostEntity.statesManager.AddState(stateAfter);
        base.StopAffect();
    }
}
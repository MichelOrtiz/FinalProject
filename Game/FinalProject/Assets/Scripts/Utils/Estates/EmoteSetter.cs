using FinalProject.Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "new EmoteSetter", menuName = "States/new EmoteSetter")]
public class EmoteSetter : State
{
    [SerializeField] private GameObject emote;
    private GameObject instantiated;

    private OffscreenIndicators offscreenIndicators;
    
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        var emotePos = manager.hostEntity.emotePos;
        instantiated = MonoBehaviour.Instantiate(emote, emotePos.position, emotePos.rotation);
        instantiated?.transform.SetParent(emotePos);

        offscreenIndicators = FindObjectOfType<OffscreenIndicators>();
        offscreenIndicators?.AddTargetIndicator(instantiated);
    }

    public override void Affect()
    {
        if (currentTime > duration)
        {
            currentTime = 0;
            StopAffect();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    public override void StopAffect()
    {
        if (instantiated != null)
        {
            Destroy(instantiated);
        }
        base.StopAffect();
    }
}
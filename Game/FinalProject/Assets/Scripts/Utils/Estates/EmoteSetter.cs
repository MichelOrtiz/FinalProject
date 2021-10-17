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
        
        //newManager.StopAll(typeof(EmoteSetter), exlude: this);
        //newManager.RemoveEmotes();

        InstantiateEmote();
        currentTime = 0;
    }

    public override void Affect()
    {
        if (currentTime > duration)
        {
            StopAffect();
            currentTime = 0;
        }
        else
        {
            if (manager.hostEntity.emotePos.childCount == 0)
            {
                InstantiateEmote();
            }
            currentTime += Time.deltaTime;
        }
    }

    public override void StopAffect()
    {
        if (instantiated != null)
        {
            Destroy(instantiated);
        }
        else
        {
            try
            {
                var emotePos = manager.hostEntity.emotePos;
                instantiated = emotePos.GetChild(0).gameObject;
                Destroy(instantiated);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex);
            }
        }
        base.StopAffect();
    }

    void InstantiateEmote()
    {
        var emotePos = manager.hostEntity.emotePos;

        instantiated = MonoBehaviour.Instantiate(emote, emotePos.position, emotePos.rotation);
        instantiated?.transform.SetParent(emotePos);

        offscreenIndicators = FindObjectOfType<OffscreenIndicators>();
        offscreenIndicators?.AddTargetIndicator(instantiated);
    }
}
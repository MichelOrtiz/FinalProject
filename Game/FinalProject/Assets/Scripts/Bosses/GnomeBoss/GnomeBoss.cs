using UnityEngine;

public class GnomeBoss : MonoBehaviour
{
    private BossFight bossFight;
    private Switch stageSwitch;
    private bool switchEventCalled;

    void Start()
    {
        bossFight = GetComponent<BossFight>();
    }

    void Update()
    {
        if (stageSwitch == null)
        {
            UpdateStageSwitch();
            switchEventCalled = false;
        }
    }

    void UpdateStageSwitch()
    {
        stageSwitch = FindObjectOfType<Switch>();
        if (stageSwitch != null)
        {
            stageSwitch.SwitchActivated += stageSwitch_Activated;
        }
    }

    void stageSwitch_Activated(Switch sender)
    {
        if (!switchEventCalled)
        {
            bossFight.NextStage();
            //stageSwitch = new Switch();
            switchEventCalled = true;
        }
    }
}

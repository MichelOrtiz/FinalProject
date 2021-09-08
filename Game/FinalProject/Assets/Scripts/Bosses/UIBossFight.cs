using UnityEngine;
using UnityEngine.SceneManagement;
public class UIBossFight : BossFight
{
    [SerializeField] private string winMessage;
    [SerializeField] private string looseTitle;
    [SerializeField] private string looseMessage;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    new void Start()
    {
        base.Start();
        PopUpUI.Instance.closedPopUp += PopUpUI_Closed;
    }

    public void LooseBattle()
    {
        popUpTrigger.popUp.Title = looseTitle;
        popUpTrigger.popUp.Message = looseMessage;
        popUpTrigger.TriggerPopUp(true);
    }

    public override void EndBattle()
    {
        if (!isCleared)
        {
            currentStage?.Destroy();
            isCleared=true;

            PlayerManager.instance.abilityManager.SetActiveSingle(ability, true);

            popUpTrigger.popUp.Title = winMessage;
            popUpTrigger.popUp.Message = ability.ToString();
            popUpTrigger.TriggerPopUp(true);
        }
        base.EndBattle();
    }

    void PopUpUI_Closed()
    {
        if (!isCleared)
        {
            // reload scene
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
        else
        {
            // return to last scene
        }
    }
}
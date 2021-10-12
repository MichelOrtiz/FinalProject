using UnityEngine;
using UnityEngine.SceneManagement;
public class UIBossFight : BossFight
{
    [SerializeField] private string winMessage;
    [SerializeField] private string looseTitle;
    [SerializeField] private string looseMessage;
    [SerializeField] private PopUpUI endBattlePopUp;

    new void Start()
    {
        base.Start();
        //PopUpUI.Instance.closedPopUp += PopUpUI_Closed;
        endBattlePopUp.closedPopUp += PopUpUI_Closed;
    }

    new void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            EndBattle();
        }
        base.Update();
    }

    public void LooseBattle()
    {
        endMessageTrigger.popUp.Title = looseTitle;
        endMessageTrigger.popUp.Message = looseMessage;
        endMessageTrigger.TriggerPopUp(true);
    }

    public override void EndBattle()
    {
        if (!isCleared)
        {
            currentStage?.Destroy();
            isCleared=true;

            PlayerManager.instance.abilityManager.SetActiveSingle(ability, true);

            endMessageTrigger.popUp.Title = winMessage;
            endMessageTrigger.popUp.Message = ability.ToString();
            endMessageTrigger.TriggerPopUp(true);

            condition.state = true;
            BattleEnded?.Invoke();

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
            ReturnToLastScene();
        }
    }
}
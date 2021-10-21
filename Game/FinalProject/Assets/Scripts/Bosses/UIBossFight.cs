using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIBossFight : BossFight
{
    [SerializeField] private string winMessage;
    [SerializeField] private string looseTitle;
    [SerializeField] private string looseMessage;
    [SerializeField] private PopUpUI endBattlePopUp;

    [SerializeField] private List<GameObject> objectsToHide;


    new void Awake()
    {
        base.Awake();
        ScenesManagers.SetListActive(objectsToHide, false);
    }

    new void Start()
    {
        base.Start();
        //PopUpUI.Instance.closedPopUp += PopUpUI_Closed;
        endBattlePopUp.closedPopUp += PopUpUI_Closed;
        PlayerManager.instance.gameObject?.SetActive(false);
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
            ScenesManagers.SetListActive(objectsToHide, true);
            PlayerManager.instance.gameObject?.SetActive(true);       
            ReturnToLastScene();
        }
    }
}
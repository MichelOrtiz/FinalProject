using System.Collections;
using System.Collections.Generic;
using FinalProject.Assets.Scripts.Utils.Sound;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public static BossFight instance;
    [SerializeField] protected PopUpTrigger startMessageTrigger;
    [SerializeField] protected PopUpTrigger endMessageTrigger;

    protected void Awake() {
        if(instance!=null){
            Debug.Log("this is bad : BossFight");
            return;
        }
        instance = this;

        //popUpTrigger = GetComponent<PopUpTrigger>();
    }
    [SerializeField] protected List<Stage> stages;
    [SerializeField] protected GameObject abilityObject;
    
    [SerializeField] protected Ability.Abilities ability;

    public Stage currentStage;
    public bool isCleared;
    protected int indexStage;

    

    protected void Start()
    {
        startMessageTrigger.TriggerPopUp(pauseGame: true);

        indexStage = 0;
        if (stages.Count > 0)
        {
            currentStage=stages[indexStage];
        }
        currentStage?.Generate();

        //AudioManager.instance?.Play("Theme");
    }
    protected void Update() {
        if(Input.GetKeyDown(KeyCode.L)){
            NextStage();
        }
        if (isCleared)
        {
            
        }
    }
    public virtual void NextStage(){
        if(indexStage<stages.Count-1){
            indexStage++;
            currentStage.Destroy();
            currentStage=stages[indexStage];
            currentStage.Generate();
        }
        else
        {
            /*Debug.Log("Lo hiciste ganaste!!!1");
            currentStage.Destroy();
            isCleared=true;*/
            //give ability
            //AbilityManager.instance.AddAbility(reward);
            EndBattle();
        }
    }

    public virtual void EndBattle()
    {
        if (!isCleared)
        {
            currentStage?.Destroy();
            isCleared=true;

            //Vector2 abilitySpawnPos = new Vector2(PlayerManager.instance.GetPosition().x, PlayerManager.instance.GetPosition().y + 5f);

            //Instantiate(abilityObject, abilitySpawnPos, abilityObject.transform.rotation);

            
            PlayerManager.instance.abilityManager.SetActiveSingle(ability, true);
            endMessageTrigger.popUp.Message = ability.ToString();
            endMessageTrigger.TriggerPopUp(true);

            Debug.Log("Lo hiciste ganaste!!!1");
        }
    }
}

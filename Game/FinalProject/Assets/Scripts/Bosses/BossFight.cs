using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public static BossFight instance;
    private void Awake() {
        if(instance!=null){
            Debug.Log("this is bad : BossFight");
            return;
        }
        instance = this;
    }
    [SerializeField] protected List<Stage> stages;
    [SerializeField] protected GameObject abilityObject;
    public Stage currentStage;
    public bool isCleared;
    protected int indexStage;

    protected void Start()
    {
        indexStage = 0;
        currentStage=stages[indexStage];
        currentStage.Generate();
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
        else{
            Debug.Log("Lo hiciste ganaste!!!1");
            currentStage.Destroy();
            isCleared=true;
            //give ability
            //AbilityManager.instance.AddAbility(reward);
        }
    }

    public virtual void EndBattle()
    {
        currentStage.Destroy();
        isCleared=true;
        Debug.Log("Lo hiciste ganaste!!!1");
    }
}

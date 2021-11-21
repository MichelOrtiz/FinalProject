using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBMaster : BossFight
{
    [SerializeField] private List<GameObject> stageBehaviours;
    [SerializeField] private GameObject currentStageBehaviour;
    private List<CastleBLamp> lamps;


    //[SerializeField] private Vector2 startBossEntityPosition;
    //public Vector2 startBossPosition { get => startBossEntityPosition; set => startBossEntityPosition = value; }
    private Vector2 currentPos;

    private CastleBStageFight stageFight;

    //private IBossFinishedBehaviour bossBehaviour;
    new void Start()
    {
        base.Start();
        lamps = ScenesManagers.GetObjectsOfType<CastleBLamp>();
        if (lamps != null)
        {
            foreach (var lamp in lamps)
            {
                lamp.ActivatedHandler += lamp_Activated;
            }
        }
        
        indexStage = 0;
        currentStage=stages[indexStage];
        currentStage.Generate();
        
        UpdateCurrentStage();
    }
    
    new void Update()
    {
        if (currentStageBehaviour != null && currentStageBehaviour.GetComponent<CastleBStageFight>() != null)
        {
            //currentPos = currentStageBehaviour.GetComponent<CastleBStageFight>().CurrentBoss.transform.position;
            currentPos = currentStageBehaviour.GetComponent<CastleBStageFight>().CurrentBoss.transform.position;
        }

        SetLampsEnabled( FindObjectOfType<DummyParalizedKing>() != null);

        base.Update();
    }

    public override void NextStage()
    {
        if(indexStage<stages.Count-1)
        {
            indexStage++;

            DestroyStageObjects();

            currentStage=stages[indexStage];
            
            currentStage.Generate();
            UpdateCurrentStage();

        }
        else
        {
            Debug.Log("Lo hiciste ganaste!!!1");
            currentStage.Destroy();
            isCleared=true;
        }
        
    }

    void UpdateCurrentStage()
    {
        currentStageBehaviour = currentStage.GenerateSingle(stageBehaviours[indexStage], currentPos);

        stageFight = currentStageBehaviour.GetComponent<CastleBStageFight>();
        stageFight.FinalStageFightEndedHandler += stageFight_FinalStageEnded;

        if (currentPos != new Vector2())
        {
            //currentStageBehaviour.GetComponent<CastleBStageFight>().startBossPosition = currentPos;
            stageFight.startBossPosition = currentPos;
        }

        //Debug.Log(currentStage.currentObjects);

        // enables lamps if currrent stage is paralized kings

    }

    void DestroyStageObjects()
    {
        //currentStageBehaviour.GetComponent<CastleBStageFight>().DestroyCurrentStage();
        stageFight.DestroyCurrentStage();
        currentStage.Destroy();
        var bullets = ScenesManagers.GetGameObjectsOfScript<StaticBullet>(); 

        if (bullets != null)
        {
            foreach (var bullet in bullets)
            {
                Destroy(bullet);
            }
        }
    }
    void lamp_Activated()
    {
        NextStage();
    }

    void SetLampsEnabled(bool value)
    {
        foreach (var lamp in lamps)
        {
            if (!lamp.activated)
            {
                lamp.canActivate = value;
            }
        }
    }

    void stageFight_FinalStageEnded()
    {
        DestroyStageObjects();
        EndBattle();
    }

    public override void EndBattle()
    {
        if (!isCleared)
        {
            currentStage?.Destroy();
            
            isCleared=true;

            Debug.Log("Lo hiciste ganaste!!!1");
            SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
            if(partida.WorldStates.Exists(x => x.id == worldState.id)){
                WorldState w = partida.WorldStates.Find(x => x.id == worldState.id);
                w.state = true;
            }else{
                worldState.state = true;
                partida.WorldStates.Add(worldState);
            }

            endMessageTrigger.TriggerPopUp(true);

            loadlevel?.SetActive(true);
            BattleEnded?.Invoke();
        }
    }
}

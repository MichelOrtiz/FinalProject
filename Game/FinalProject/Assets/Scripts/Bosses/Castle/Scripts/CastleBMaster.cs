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

    //private IBossFinishedBehaviour bossBehaviour;

    void Awake()
    {
        
    }

    new void Start()
    {
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
        if (currentStageBehaviour.GetComponent<CastleBStageFight>() != null)
        {
            currentPos = currentStageBehaviour.GetComponent<CastleBStageFight>().CurrentBoss.transform.position;
        }
        Debug.Log("Dummy king: " +  currentStageBehaviour.GetComponent<CastleBStageFight>().CurrentBoss.GetComponentInChildren<DummyParalizedKing>() != null);

        base.Update();
    }

    public override void NextStage()
    {
        if(indexStage<stages.Count-1)
        {
            indexStage++;

            currentStageBehaviour.GetComponent<CastleBStageFight>().DestroyCurrentStage();
            currentStage.Destroy();
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
        if (currentPos != new Vector2())
        {
            currentStageBehaviour.GetComponent<CastleBStageFight>().startBossPosition = currentPos;
        }

        foreach (var obj in currentStage.currentObjects)
        {
            Debug.Log(obj);
        }
        //Debug.Log(currentStage.currentObjects);

        // enables lamps if currrent stage is paralized kings

        //SetLampsEnabled( currentStageBehaviour.GetComponent<CastleBStageFight>().CurrentBoss.GetComponentInChildren<DummyParalizedKing>() != null);
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
}

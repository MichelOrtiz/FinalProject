using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornHead : MonoBehaviour
{
    //[SerializeField] private BossFight bossFight;
    PlayerManager player;
    GroundChecker playerGroundChecker;  
    [SerializeField] private List<UBLamp> lamps;
    [SerializeField] private GameObject child;
    private GameObject nextChild;

    [SerializeField] private GameObject GroundAttack;
    [SerializeField] private GameObject PlatformAttack;
    [SerializeField] private GameObject RainAttack;

    [SerializeField] private float timeBtwAttacks;
    private float currentTimeBtwAttack;
    private bool changingAttack;

    int lampsActivated;

    void Awake()
    {
        lamps = ScenesManagers.GetObjectsOfType<UBLamp>();
    }

    void Start()
    {
        player = PlayerManager.instance;
        playerGroundChecker = player.groundChecker;

        foreach (var lamp in lamps)
        {
            lamp.ActivatedHandler += lamp_Activated;
        }

        GroundAttack.GetComponent<UBBehaviour>().FinishedAttackHandler += GroundAttack_FinishedAttack;
        PlatformAttack.GetComponent<UBBehaviour>().FinishedAttackHandler += PlatformAttack_FinishedAttack;
        RainAttack.GetComponent<UBBehaviour>().FinishedAttackHandler += RainAttack_FinishedAttack;


        playerGroundChecker.ChangedGroundTagHandler += playerGroundChecker_ChangedGroundTagHandler;

        //playerGroundChecker_ChangedGroundTagHandler();
        nextChild = GroundAttack;
        ChangeChild(nextChild);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChildrenPosition();
        if (AllLampsActivated())
        {
            FindObjectOfType<BossFight>().EndBattle();
        }
        if (changingAttack)
        {
            if (currentTimeBtwAttack > timeBtwAttacks)
            {
                ChangeChild(nextChild);
                currentTimeBtwAttack = 0;
                changingAttack = false;
            }
            else
            {
                currentTimeBtwAttack += Time.deltaTime;
            }
        }
        else
        {
            transform.position = child.transform.localPosition; 
        }
    }

    void UpdateChildrenPosition()
    {
        if (child != GroundAttack) GroundAttack.transform.localPosition = child.transform.localPosition;
        else if (child != PlatformAttack) PlatformAttack.transform.parent.localPosition = child.transform.localPosition;
        else if (child != RainAttack) RainAttack.transform.localPosition = child.transform.localPosition; 
    }

    void playerGroundChecker_ChangedGroundTagHandler()
    {
        if (playerGroundChecker.lastGroundTag == "Ground")
        {
            
            if (child != GroundAttack && nextChild != RainAttack)
            {
                Debug.Log("Changed attack to ground");
                nextChild = GroundAttack;
                //ChangeChild(GroundAttack);
            }
        }
        else if (playerGroundChecker.lastGroundTag == "Platform")
        {
            if (child != PlatformAttack)
            {
                Debug.Log("Changed attack to platform");
                nextChild = PlatformAttack;
                //ChangeChild(PlatformAttack);
            }
        }
    }

    void lamp_Activated()
    {
        if (child != RainAttack)
        {
            nextChild = RainAttack;
        }
        lampsActivated++;
        if (lampsActivated > 1)
        {
            RainAttack.GetComponent<UBAttackedBehaviour>().ModValues();
        }
        /*if (!changingAttack)
        {
            ChangeChild(RainAttack);
        }*/
    }

    bool AllLampsActivated()
    {
        return lampsActivated == lamps.Count;  
    }

    void ChangeChild(GameObject gameObject)
    {
        //child.SetActive(false);
        //child = gameObject;
        //child.SetActive(true);
        if (child != null)
        {
            child.GetComponent<UBBehaviour>().SetActive(false);
        
        }
        
        /*if (gameObject != GroundAttack)
        {
            Destroy(FindObjectOfType<Laser>()?.gameObject);
        }*/


        child = gameObject;
        child.GetComponent<UBBehaviour>().SetActive(true);


        gameObject.transform.localPosition = transform.position;
    }

    void FinishChildAttack()
    {
        child.GetComponent<UBBehaviour>().FinishAttack();
    }

    void GroundAttack_FinishedAttack()
    {
        changingAttack = true;
        playerGroundChecker_ChangedGroundTagHandler();

    }

    void PlatformAttack_FinishedAttack()
    {
        changingAttack = true;
        playerGroundChecker_ChangedGroundTagHandler();

    }

    void RainAttack_FinishedAttack()
    {
        nextChild = GroundAttack;
        changingAttack = true;
        playerGroundChecker_ChangedGroundTagHandler();
    }


    void OnDestroy()
    {
        EntityDestroyFx.Instance.StartDestroyFx(child);
    }
}

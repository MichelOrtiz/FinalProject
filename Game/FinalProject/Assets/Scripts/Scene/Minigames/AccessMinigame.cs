using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessMinigame : MonoBehaviour
{
    public float radius;
    public GameObject minigameObject;
    public Minigame Minigame { get; private set; }

    public MasterMinigame MasterMinigame { get; set; }
    //[SerializeReference] private bool isCompleted;
    public bool available;
    [SerializeField] private float cooldownTime;
    private float curCooldownTime;

    public bool InCooldown { get => curCooldownTime > 0; }

    private bool minigameInstantiated;

    [SerializeField] private GameObject signInter;
    [SerializeField] private GameObject signCooldown;

    
    // Start is called before the first frame update
    void Start()
    {
        available = true;
        PlayerManager.instance.inputs.Interact += Inputs_interact ;
        signCooldown.SetActive(false);
    }
    private void OnDestroy() {
        PlayerManager.instance.inputs.Interact -= Inputs_interact ;
    }

    // Update is called once per frame
    void Update()
    {
        //Place where the oject is - place where Nico is
        

        if (!available)
        {
            if (signInter != null) signInter.SetActive(false);

            if (curCooldownTime > cooldownTime)
            {
                available = true;
                curCooldownTime = 0;
            }
            else
            {
                curCooldownTime += Time.deltaTime;
            }
        }
        float distance = Vector2.Distance(PlayerManager.instance.transform.position, transform.position);
        if(!minigameInstantiated && distance<radius)
        {
            if (signInter != null && available)
            {
                signInter.SetActive(true);
                signCooldown.SetActive(false);
            }
            else if (signCooldown != null && !available)
            {
                signInter.SetActive(false);
                signCooldown.SetActive(true);
            }
        }
        else
        {
            if (signInter != null) signInter.SetActive(false);
            if (signCooldown != null) signCooldown.SetActive(false);
        }
        
    }

    void minigame_Ended()
    {
        available = false;
        minigameInstantiated = false;
    }

    void masterMinigame_WinMinigame()
    {
        //isCompleted = true;
        Minigame.EndMinigame(true);
    }

    void masterMinigame_LoseMinigame()
    {
        Minigame.EndMinigame(false);
    }

    void Inputs_interact(){
        if (!available)
        {
            return;
        }
        float distance = Vector2.Distance(PlayerManager.instance.transform.position, transform.position);
        if(!minigameInstantiated && distance<radius)
        {
            minigameInstantiated = true;
            Debug.Log("should access minigame from " + minigameObject);
            //spawns the minigame as a Unity object so that it recognizes its methods, then runs its code.
            Minigame = Instantiate(minigameObject).GetComponent<Minigame>();  
            Minigame.StartMinigame();
            
            Minigame.MinigameEndedHandler += minigame_Ended;

            MasterMinigame = Minigame.MasterMinigame;

            if (MasterMinigame != null)
            {
                MasterMinigame.WinMinigameHandler += masterMinigame_WinMinigame;
                MasterMinigame.LoseMinigameHandler += masterMinigame_LoseMinigame;
            }
            else
            {
                Debug.Log("Master minigame null smh");
            }
        }
    }
}

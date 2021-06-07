using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessMinigame : MonoBehaviour
{
    public float radius;
    public GameObject minigameObject;
    public Minigame minigame;

    public MasterMinigame MasterMinigame { get; set; }
    //[SerializeReference] private bool isCompleted;
    [SerializeField] private bool available;
    [SerializeField] private float cooldownTime;
    private float curCooldownTime;
    
    // Start is called before the first frame update
    void Start()
    {
        available = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Place where the oject is - place where Nico is
        float distance = Vector2.Distance(PlayerManager.instance.transform.position, transform.position);

        if (!available)
        {
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
        else if(Input.GetKeyDown(KeyCode.E)&&distance<radius)
        {
            //spawns the minigame as a Unity object so that it recognizes its methods, then runs its code.
            minigame = Instantiate(minigameObject).GetComponent<Minigame>();  
            minigame.StartMinigame();
            
            minigame.MinigameEndedHandler += minigame_Ended;

            MasterMinigame = minigame.MasterMinigame;

            if (MasterMinigame != null)
            {
                MasterMinigame.WinMinigameHandler += masterMinigame_WinMinigame;
            }
            else
            {
                Debug.Log("Master minigame null smh");
            }
        }
        
    }

    void minigame_Ended()
    {
        available = false;
    }

    void masterMinigame_WinMinigame()
    {
        //isCompleted = true;
        minigame.EndMinigame(true);
    }
}

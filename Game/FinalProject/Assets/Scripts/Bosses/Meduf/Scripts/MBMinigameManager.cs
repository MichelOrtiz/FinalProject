using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBMinigameManager : MonoBehaviour
{
    public GameObject currentHost; 
    private GameObject lastHost;
    [SerializeField] private List<AccessMinigame> minigameAccess;
    [SerializeReference] private AccessMinigame currentMinigameAccess;
    [SerializeReference] private byte index;
    private bool assignedEvent;
    [SerializeField] private float timeBtwMinigames;

    private MBPartsHandler partsHandler;

    [Header ("Machine FX")]
    [SerializeField] private SpriteRenderer machineFx;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    #region Events
    public delegate void AllMinigamesCompleted();
    public event AllMinigamesCompleted AllMinigamesCompletedHandler;
    protected virtual void OnAllMinigamesCompleted()
    {
        AllMinigamesCompletedHandler?.Invoke();
    }
    #endregion

    void Start()
    {
        partsHandler = FindObjectOfType<MBPartsHandler>();
        if (partsHandler == null)
        {
            Debug.Log("why");
        }
        partsHandler.ChangedReferenceHandler += partsHandler_ChangedReference;
        
        SetMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMinigameAccess != null)
        {
            if (currentMinigameAccess.MasterMinigame != null)
            {
                // so it is only assigned once per object
                if (!assignedEvent)
                {
                    currentMinigameAccess.MasterMinigame.WinMinigameHandler += access_WinMinigame;
                    assignedEvent = true;
                }
            }

            if (!currentMinigameAccess.available)
            {
                Debug.Log("In cooldown");
                if (machineFx.color == enabledColor) machineFx.color = disabledColor;
            }
            else
            {
                if (machineFx.color == disabledColor) machineFx.color = enabledColor;
            }

        }

        if (FindObjectOfType<Minigame>() == null)
        {
            assignedEvent = false;
        }
    }

    void SetMinigame()
    {
        if (currentHost == null)
        {
            currentHost = partsHandler.CurrentReference.transform.parent.gameObject;
        }
        // FindObjectIOfType<AccessMinigame>());

        ScenesManagers.GetComponentsInChildrenList<AccessMinigame>(currentHost)?.ForEach(m => Destroy(m.gameObject));
        Destroy(currentMinigameAccess);

        currentMinigameAccess = Instantiate(minigameAccess[index], currentHost.transform);
        machineFx = currentHost.GetComponent<MBJumper>().MachineFx.GetComponent<SpriteRenderer>();
        assignedEvent = false;
        
        machineFx.color = enabledColor;
    }

    void partsHandler_ChangedReference(GameObject reference)
    {
        lastHost = currentHost;
        currentHost = reference.transform.parent.gameObject;
        SetMinigame();
    }

    void access_WinMinigame()
    {
        Invoke("HandleWonMinigame", timeBtwMinigames);
    }

    void HandleWonMinigame()
    {
        if (index < minigameAccess.Count-1)
        {
            index++;
            SetMinigame();
        }
        else
        {
            OnAllMinigamesCompleted();
        }
    }

    void currentMinigame_Ended()
    {
        Debug.Log("ended minigame " + currentMinigameAccess.minigameObject.GetComponent<Minigame>());
        assignedEvent = false;
    }
}

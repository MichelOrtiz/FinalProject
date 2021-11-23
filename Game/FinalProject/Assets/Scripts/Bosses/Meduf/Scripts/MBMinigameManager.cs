using System;
using System.Collections.Generic;
using UnityEngine;

public class MBMinigameManager : MonoBehaviour
{
    public GameObject currentHost; 
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
    public Action AllMinigamesCompleted;
    #endregion

    void Start()
    {
        partsHandler = FindObjectOfType<MBPartsHandler>();
        if (partsHandler == null)
        {
            Debug.Log("why");
        }
        partsHandler.ChangedReference += partsHandler_ChangedReference;
        
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
        currentHost = reference.transform.parent.gameObject;
        currentHost.GetComponent<MBJumper>().accessMinigame = currentMinigameAccess;
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
            AllMinigamesCompleted?.Invoke();
        }
    }
}

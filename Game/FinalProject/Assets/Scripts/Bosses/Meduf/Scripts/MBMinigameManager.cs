using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBMinigameManager : MonoBehaviour
{
    [SerializeReference] private GameObject currentHost; 
    private GameObject lastHost;
    [SerializeField] private List<AccessMinigame> minigameAccess;
    [SerializeReference] private AccessMinigame currentMinigameAccess;
    [SerializeReference] private byte index;
    private bool assignedEvent;

    private MBPartsHandler partsHandler;

    #region Events
    public delegate void AllMinigamesCompleted();
    public event AllMinigamesCompleted AllMinigamesCompletedHandler;
    protected virtual void OnAllMinigamesCompleted()
    {
        AllMinigamesCompletedHandler?.Invoke();
    }
    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        
    }

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
        if (currentMinigameAccess?.MasterMinigame != null)
        {
            // so it is only assigned once per object
            if (!assignedEvent)
            {
                currentMinigameAccess.MasterMinigame.WinMinigameHandler += access_WinMinigame;
                assignedEvent = true;
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
        assignedEvent = false;
    }

    void partsHandler_ChangedReference(GameObject reference)
    {
        lastHost = currentHost;
        currentHost = reference.transform.parent.gameObject;
        SetMinigame();
    }

    void access_WinMinigame()
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

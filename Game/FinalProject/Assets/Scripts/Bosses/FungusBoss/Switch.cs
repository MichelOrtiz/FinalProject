using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool activado;
    [SerializeField] private float radius;
    [SerializeField] private List<GameObject> doorsAttached;
    public static List<Door> AllDoors { get; set; }
    private PlayerManager player;

    #region Time
    [Header("Time")]
    [SerializeField] private bool hasTime;
    [SerializeField] private float timeActivated;
    private float curTimeActivated;
    #endregion

    // Events :O
    public delegate void Activate(Switch sender);
    public event Activate SwitchActivated;
    protected virtual void OnSwitchActivated(Switch sender)
    {
        SwitchActivated?.Invoke(sender);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        AllDoors = ScenesManagers.GetObjectsOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.GetPosition(), transform.position);
        if (distanceFromPlayer <= radius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activado = !activado;
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                foreach (var door in doorsAttached)
                {
                    if (door != null)
                    {
                        if (AllDoors.Exists(d => d.name.Equals(door.name)))
                        {
                            door.GetComponent<Door>().Activate();
                        }
                    }
                }

                // Events :0
                OnSwitchActivated(this);
            }
        }
        if (activado)
        {
            if (curTimeActivated > timeActivated)
            {
                DeActivate();
                curTimeActivated = 0;
            }
            else
            {
                curTimeActivated += Time.deltaTime;
            }
        }
    }

    public void DeActivate()
    {
        if (activado)
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            activado = false;
            foreach (var door in doorsAttached)
            {
                if (door != null)
                {
                    if (AllDoors.Exists(d => d.name.Equals(door.name)))
                    {
                        if(door.GetComponent<Door>().isOpen)
                        {
                            door.GetComponent<Door>().Activate();
                        }
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

using UnityEngine;
[RequireComponent(typeof(Reflex))]
[RequireComponent(typeof(MirrorReflex))]
public class CloneActivator : MonoBehaviour
{
    [SerializeField] private Transform mirror;
    [SerializeField] private float distanceToActivate;
    [SerializeField] private GameObject warningSign;
    private MirrorReflex mirrorReflex;
    private Reflex reflex;   
    
    private PlayerManager player;

    void Awake()
    {
        reflex = GetComponent<Reflex>();
        mirrorReflex = GetComponent<MirrorReflex>();
    }
    
    void Start()
    {
        player = PlayerManager.instance;
    }

    void Update()
    {
        if (MathUtils.GetAbsXDistance(mirror.position, player.GetPosition()) <= distanceToActivate)
        {
            if (!reflex.enabled)
            {
                Instantiate(warningSign, player.GetPosition(), warningSign.transform.rotation);
                reflex.enabled = true;
                mirrorReflex.enabled = false;
            }
        }
    }
}
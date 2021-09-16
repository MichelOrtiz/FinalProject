using UnityEngine;
[RequireComponent(typeof(Reflex))]
[RequireComponent(typeof(MirrorReflex))]
public class CloneActivator : MonoBehaviour
{
    [SerializeField] private Transform mirror;
    [SerializeField] private float distanceToActivate;
    [SerializeField] private GameObject warningSign;
    [SerializeField] private AnimationManager animationManager;

    [SerializeField] private float cloneDelayIncrease;

    [SerializeField] private FieldOfView fieldOfView;
    private MirrorReflex mirrorReflex;
    private Reflex reflex;   
    
    private PlayerManager player;

    void Awake()
    {
        reflex = GetComponent<Reflex>();
        mirrorReflex = GetComponent<MirrorReflex>();
        animationManager.Entity = mirrorReflex;
        
        mirrorReflex.animationManager = animationManager;
        reflex.animationManager = animationManager;
    }
    
    void Start()
    {
        player = PlayerManager.instance;
    }

    void Update()
    {
        //if (Vector2.Distance(transform.position, player.GetPosition()) <= distanceToActivate)
        if (Vector2.Distance(transform.position, player.GetPosition()) <= distanceToActivate)
        {
            if (!reflex.enabled)
            {
                Instantiate(warningSign, player.GetPosition(), warningSign.transform.rotation);
                animationManager.Entity = reflex;
                reflex.enabled = true;
                mirrorReflex.enabled = false;

                var reflexs = ScenesManagers.GetObjectsOfType<Reflex>().FindAll(r => r.enabled);
                if (reflexs != null && reflexs.Count > 1)
                {
                    foreach (var rf in reflexs)
                    {
                        reflex.delay += cloneDelayIncrease;
                    }
                }
            }
        }
    }
}
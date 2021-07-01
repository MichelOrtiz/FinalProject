using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    [SerializeField] private GameObject originalReference;
    public GameObject OriginalReference
    {
        get { return originalReference; }
        set { originalReference = value; }
    }
    
    public GameObject ReferenceObject { get; private set; }
    
    private PlayerManager player;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = PlayerManager.instance;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0, 1f);
    }


    void Update()
    {
        spriteRenderer.enabled = ReferenceObject != null;
        if (ReferenceObject == null)
        {
            if (originalReference != null)
            {
                ReferenceObject = ScenesManagers.FindGameObject(g => g == originalReference);
            }
        }
        else
        {
            //float angle = MathUtils.GetAngleBetween(transform.position, referenceSwitch.transform.position);
            float angle = MathUtils.GetAngleBetween(transform.position, ReferenceObject.transform.position);
            transform.eulerAngles = new Vector3(0,0,angle);
            //transform.eulerAngles = transform.TransformVector(MathUtils.GetVectorFromAngle(angle));
        }
    }
}

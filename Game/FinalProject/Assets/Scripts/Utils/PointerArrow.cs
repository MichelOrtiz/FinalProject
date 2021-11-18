using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    [SerializeField] private bool rotateToReference;
    [SerializeField] private GameObject originalReference;
    public GameObject OriginalReference
    {
        get { return originalReference; }
        set 
        {
            originalReference = value; 
            rotateToReference = true;
            rotateToDirection = false;
        }
    }
    
    public GameObject ReferenceObject { get => ReferenceObject; set 
        {
            ReferenceObject = value; 
            rotateToReference = true;
            rotateToDirection = false;
        }
    }

    [SerializeField] private bool rotateToDirection;
    [SerializeField] private Vector2 referenceDir;
    public Vector2 ReferenceDir{ get=> referenceDir; set 
        {
            referenceDir = value; 
            rotateToDirection = true;
            rotateToReference = false;
        }
    }


    private PlayerManager player;

    
    private SpriteRenderer spriteRenderer;


    [SerializeField] private float startAngle;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = PlayerManager.instance;
        //ransform.SetParent(player.transform);
        //transform.localPosition = new Vector2(0, 1f);

        if (rotateToDirection)
        {
            spriteRenderer.enabled = ReferenceDir != null;
            if (ReferenceDir != null)
            {
                //float angle = MathUtils.GetAngleBetween(transform.position, referenceSwitch.transform.position);
                float angle = MathUtils.GetAngleBetween(transform.position, ReferenceDir);
                transform.eulerAngles = new Vector3(0,0,angle + startAngle);
                //transform.eulerAngles = transform.TransformVector(MathUtils.GetVectorFromAngle(angle));
            }
        }
    }


    void Update()
    {
        if (rotateToReference)
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
                transform.eulerAngles = new Vector3(0,0,angle + startAngle);
                //transform.eulerAngles = transform.TransformVector(MathUtils.GetVectorFromAngle(angle));
            }
        }
    }
}

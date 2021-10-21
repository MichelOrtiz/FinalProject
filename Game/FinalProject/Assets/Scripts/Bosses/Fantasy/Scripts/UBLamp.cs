
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class UBLamp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteWhenEnabled;
    [SerializeField] private Sprite spriteWhenDisabled;

    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Light2D light2D;

    
    [SerializeField] private float interactionRadius;
    //[SerializeField] private float effectRadius;
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private string targetObjectTag;
    [SerializeField] private float waitTimeIfFailActivate;
    private float currentTime;
    private bool targetEnteredZone;
    private bool canActivate;

    #region Events
    public delegate void Activated();
    public event Activated ActivatedHandler;
    protected virtual void OnActivated()
    {
        ActivatedHandler?.Invoke();
        
        light2D.enabled = false;
        collisionHandler.gameObject.SetActive(false);
        enabled = false;
    }

    #endregion
    private PlayerManager player;

    void Awake()
    {
        if (collisionHandler != null)
        {
            //collisionHandler.StayTouchingContactHandler += collisionHandler_StayTouchingContact;
            collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterTouchingContact;
            collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitTouchingContact;
        }
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //ChangeSprite(spriteWhenEnabled);
        light2D.enabled = false;

        collisionHandler.gameObject.SetActive(false);
    }

    void Start()
    {
        player = PlayerManager.instance;
        canActivate = true;

        player.inputs.Interact += inputs_Interact;
    }

    // Update is called once per frame
    void Update()
    {
        //distanceFromTarget = Vector2.Distance(transform.position, targetObject.position);
        //Debug.Log(targetObject.position);
        if (canActivate)
        {
            //ChangeSprite(spriteWhenEnabled);
        }
        else
        {
            if (currentTime > waitTimeIfFailActivate)
            {
                currentTime = 0;
                light2D.enabled = false;
                collisionHandler.gameObject.SetActive(false);

                canActivate = true;
                ChangeSpriteColor(enabledColor);

            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    void inputs_Interact()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, player.GetPosition());
        if (distanceFromPlayer > interactionRadius || !canActivate) return;

        //ChangeSprite(spriteWhenDisabled);
        light2D.enabled = true;
        collisionHandler.gameObject.SetActive(true);

        ChangeSpriteColor(disabledColor);
        /*else
        {
        }*/
        canActivate = false;
    }

    void ChangeSprite(Sprite sprite)
    {
        if (spriteRenderer.sprite != sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    void ChangeSpriteColor(Color color)
    {
        if (spriteRenderer.color != color)
        {
            spriteRenderer.color = color;
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, effectRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    void collisionHandler_EnterTouchingContact(GameObject contact)
    {
        if (contact.tag == targetObjectTag)
        {
            targetEnteredZone = true;
        }
        if (targetEnteredZone)
        {
            OnActivated();
            targetEnteredZone = false;
        }
    }

    void collisionHandler_ExitTouchingContact(GameObject contact)
    {
        if (contact.tag == targetObjectTag)
        {
            targetEnteredZone = false;
        }
    }

    void OnDestroy()
    {
        player.inputs.Interact -= inputs_Interact;
    }
}

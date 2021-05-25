using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UBLamp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteWhenEnabled;
    [SerializeField] private Sprite spriteWhenDisabled;
    
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
        enabled = false;
    }

    #endregion
    private PlayerManager player;

    void Awake()
    {
        if (collisionHandler != null)
        {
            collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterTouchingContact;
            collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitTouchingContact;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSprite(spriteWhenEnabled);
    }

    void Start()
    {
        player = PlayerManager.instance;
        canActivate = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, player.GetPosition());
        //distanceFromTarget = Vector2.Distance(transform.position, targetObject.position);
        //Debug.Log(targetObject.position);
        if (canActivate)
        {
            ChangeSprite(spriteWhenEnabled);
            if (distanceFromPlayer <= interactionRadius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ChangeSprite(spriteWhenDisabled);
                    if (targetEnteredZone)
                    {
                        OnActivated();
                    }
                    else
                    {
                        canActivate = false;
                    }
                }
            }
        }
        else
        {
            if (currentTime > waitTimeIfFailActivate)
            {
                canActivate = true;
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    void ChangeSprite(Sprite sprite)
    {
        if (spriteRenderer.sprite != sprite)
        {
            spriteRenderer.sprite = sprite;
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
    }

    void collisionHandler_ExitTouchingContact(GameObject contact)
    {
        if (contact.tag == targetObjectTag)
        {
            targetEnteredZone = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBLamp : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    public bool canActivate;
    public bool activated;

    [SerializeField] private Sprite spriteWhenEnabled;
    [SerializeField] private Sprite spriteWhenDisabled;
    private SpriteRenderer spriteRenderer;

    public delegate void Activated();
    public event Activated ActivatedHandler;
    protected virtual void OnActivated()
    {
        activated = true;
        canActivate = false;
        ActivatedHandler?.Invoke();        
    }

    private PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite(canActivate? spriteWhenEnabled : spriteWhenDisabled);
        if (canActivate)
        {
            if (Vector2.Distance(player.GetPosition(), transform.position) <= interactionRadius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnActivated();
                }
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

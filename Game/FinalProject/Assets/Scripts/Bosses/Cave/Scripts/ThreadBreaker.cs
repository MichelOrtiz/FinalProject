using UnityEngine;
using System.Collections.Generic;
public class ThreadBreaker : MonoBehaviour
{
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite activatedSprite;

    [SerializeField] private float radius;
    [SerializeField] private List<LineRenderer> linesAttached;

    [SerializeField] private GameObject signInter;

    private PlayerManager player;
    private SpriteRenderer spriteRenderer;


    public bool isActivated { get; private set;}

    public delegate void Activated();
    public event Activated ActivatedHandler;
    protected virtual void OnActivated()
    {
        ActivatedHandler?.Invoke();
    }

    void Start()
    {
        player = PlayerManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
        if (signInter != null) signInter.SetActive(false);
    }

    void Update()
    {
        if (!isActivated)
        {
            float distance = Vector2.Distance(player.GetPosition(), transform.position);
            if (distance <= radius)
            {
                if (signInter != null) signInter.SetActive(true);
                if (Input.GetKeyDown(player.inputs.controlBinds["MENUINTERACTION"]))
                {
                    isActivated = true;
                    spriteRenderer.sprite = activatedSprite;
                    DestroyLines();
                }
            }
            else
            {
                if (signInter != null) signInter.SetActive(false);
            }
        }
        else
        {
            if (signInter != null) signInter.SetActive(false);
        }
    }

    void DestroyLines()
    {
        foreach (var line in linesAttached)
        {
            Destroy(line);
        }
        OnActivated();
    }
    
}
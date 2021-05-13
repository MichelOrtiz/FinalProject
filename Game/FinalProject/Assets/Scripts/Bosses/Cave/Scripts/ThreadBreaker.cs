using UnityEngine;
using System.Collections.Generic;
public class ThreadBreaker : MonoBehaviour
{
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite activatedSprite;

    [SerializeField] private float radius;
    [SerializeField] private List<LineRenderer> linesAttached;

    private PlayerManager player;
    private SpriteRenderer spriteRenderer;


    public bool isActivated { get; private set;}


    public delegate void Activated();
    public event Activated ActivatedHandler;
    protected virtual void OnActivated()
    {
        ActivatedHandler?.Invoke();
    }

    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = PlayerManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!isActivated)
        {
            float distance = Vector2.Distance(player.GetPosition(), transform.position);
            if (distance <= radius)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = activatedSprite;
                    DestroyLines();
                }
            }
        }
    }

    void DestroyLines()
    {
        foreach (var line in linesAttached)
        {
            Destroy(line);
        }
        isActivated = true;
        OnActivated();
    }
    
}
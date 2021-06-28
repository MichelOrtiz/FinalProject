using UnityEngine;
using System;
public class ProjectileDeflector : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Color colorWhenInRadius;
    [SerializeField] private Color colorWhenHit;

    #region Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Projectile projectile;
    private BlinkingSprite blinkingSprite;
    #endregion

    #region Currents
    private Vector2 currentVelocity;
    private Color defaultColor;
    private bool projectileHit;
    #endregion

    #region References
    private PlayerManager player;
    #endregion
    

    void Awake()
    {
        //projectile = GetComponent<Projectile>().gameObject;
        player = PlayerManager.instance;
        projectile = GetComponent<Projectile>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkingSprite = GetComponent<BlinkingSprite>();
    }

    void Start()
    {
        currentVelocity = rb.velocity;
        defaultColor = spriteRenderer.color;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.GetPosition());
        if (distance <= interactionRadius)
        {
            spriteRenderer.color = colorWhenInRadius;
            if (Input.GetMouseButtonDown(0))
            {
                projectileHit = true;
                float speed = currentVelocity.magnitude;
                //var direction = GameCamera.instance.GetMousePosition();

                 //Vector2.Reflect(currentVelocity.normalized, transform.position.normalized); 
                projectile.Setup(player.transform,  player.GetComponentInChildren<MouseDirPointer>().PointerDir );
                projectile.speedMultiplier *= speedMultiplier;
                    //rb.velocity = transform.InverseTransformVector(direction.normalized) * GetComponent<Projectile>().speedMultiplier * speedMultiplier;
            }
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }

        if (projectileHit)
        {
            spriteRenderer.color = colorWhenHit;
            if (blinkingSprite != null)
            {
                blinkingSprite.enabled = false;
            }
        }

    }
}
using UnityEngine;
using System;
public class ProjectileDeflector : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    [SerializeField] private float interactionAngle;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Color spriteModifier;
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

    public bool Deflected { get; set; }
    #endregion

    #region References
    private PlayerManager player;
    private MouseDirPointer mouseDirPointer;


    private Vector2 projectileDirection;
    private float angle;
    private float projectileAngle;
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
        defaultColor = spriteModifier;
        spriteRenderer.color = defaultColor;

        mouseDirPointer = FindObjectOfType<MouseDirPointer>();


        projectileDirection = projectile.shootDir;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.GetPosition());

        Debug.DrawRay(transform.position, projectileDirection);

        //angle = MathUtils.GetAngleBetween(transform.position, mouseDirPointer.PointerDir);
        //projectileAngle = MathUtils.GetAngleFromVectorFloat(projectile.shootDir);

        if (distance <= interactionRadius)
        {
            spriteRenderer.color = colorWhenInRadius;
            if (Input.GetMouseButtonDown(0) && !Deflected)
            {
                projectileHit = true;
                float speed = currentVelocity.magnitude;
                //var direction = GameCamera.instance.GetMousePosition();

                 //Vector2.Reflect(currentVelocity.normalized, transform.position.normalized); 
                projectile.Setup(player.transform,  player.GetComponentInChildren<MouseDirPointer>().PointerDir );
                projectile.speedMultiplier *= speedMultiplier;

                Deflected = true;
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
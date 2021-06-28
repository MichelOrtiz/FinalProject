using UnityEngine;
using System;
public class ProjectileDeflector : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    [SerializeField] private float speedMultiplier;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Projectile projectile;
    private PlayerManager player;

    void Awake()
    {
        //projectile = GetComponent<Projectile>().gameObject;
        player = PlayerManager.instance;
        projectile = GetComponent<Projectile>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentVelocity = rb.velocity;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.GetPosition());
        if (distance <= interactionRadius)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float speed = currentVelocity.magnitude;
                //var direction = GameCamera.instance.GetMousePosition();

                 //Vector2.Reflect(currentVelocity.normalized, transform.position.normalized); 
                projectile.Setup(player.transform,  player.GetComponentInChildren<MouseDirPointer>().PointerDir );
                projectile.speedMultiplier *= speedMultiplier;
                    //rb.velocity = transform.InverseTransformVector(direction.normalized) * GetComponent<Projectile>().speedMultiplier * speedMultiplier;

            }
        }
    }
}
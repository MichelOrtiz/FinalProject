using UnityEngine;
using System;
public class ProjectileDeflector : MonoBehaviour
{
    [SerializeField] private float interactionRadius;
    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Projectile projectile;
    private PlayerManager player;

    void Awake()
    {
        //projectile = GetComponent<Projectile>().gameObject;
        player = PlayerManager.instance;
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
                var direction = GameCamera.instance.GetMousePosition();

                 //Vector2.Reflect(currentVelocity.normalized, transform.position.normalized); 
                GetComponent<Projectile>().Setup(player.transform,  direction);
                rb.velocity = direction.normalized * GetComponent<Projectile>().speedMultiplier * 1.5f;

            }
        }
    }
}
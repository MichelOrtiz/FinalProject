
using UnityEngine;
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool drainAllStamina;
    private PlayerManager player;
    void Start()
    {
        player = PlayerManager.instance;

        if (drainAllStamina)
        {
            damage = player.maxStamina;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.TakeTirement(damage);
            if (damage > 0)
            {
                player.SetImmune();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.TakeTirement(damage);
            if (damage > 0)
            {
                player.SetImmune();
            }
        }
    }
}

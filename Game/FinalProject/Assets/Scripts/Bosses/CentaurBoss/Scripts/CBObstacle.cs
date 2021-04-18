
using UnityEngine;
public class CBObstacle : MonoBehaviour
{
    [SerializeField] private float damage;
    private PlayerManager player;
    void Start()
    {
        player = PlayerManager.instance;
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
        }
    }
}

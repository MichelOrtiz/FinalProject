using UnityEngine;
public class EnemyCollisionHandler : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] private LayerMask whatIsGround;
    //[SerializeField] private LayerMask whatIsObstacle;

    [SerializeField] private BoxCollider2D groundCollider;
    [SerializeField] private Collider2D triggerCollider;

    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkFeetRadius;



    public bool isGrounded;

    private PlayerManager player;
    void Awake()
    {
        
    }
    void Start()
    {
        player = PlayerManager.instance;
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.touchingPlayer = true;
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.touchingPlayer = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBJumper : MonoBehaviour
{
    #region Physics
    [Header("Physics")]
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private float timeBtwJump;
    private float curTimeBtwJump;
    [SerializeField] private float translationSpeed;
    #endregion

    #region Rigid, Colliders
    [Header("Rigid, Colliders")]
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private GroundChecker groundChecker;
    private Rigidbody2D rb;
    [SerializeReference]private bool justGrounded;
    #endregion

    #region References
    [Header("References")]
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private MBPartsHandler partsHandler;
    [SerializeField] private GameObject positionReference;
    [SerializeField] private GameObject machineFx;
    public GameObject MachineFx { get => machineFx; }

    [SerializeField] private AccessMinigame accessMinigame;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color interactableColor;
    [SerializeField] private Color notInteractColor;

    private PlayerManager player;
    public bool isReference;
    public bool inStartPos;
    #endregion    


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //partsHandler = GetComponent<MBPartsHandler>();
    
        collisionHandler.StayTouchingContactHandler += collisionHandler_TouchingContact;
        groundChecker.GroundedHandler += groundChecker_Grounded;
        partsHandler.ChangedReferenceHandler += partsHandler_ChangedReference;
    }

    // Update is called once per frame
    void Update()
    {
        if (accessMinigame == null)
        {
            accessMinigame = GetComponentInChildren<AccessMinigame>();
        }

        inStartPos = Vector2.Distance((Vector2)transform.position, initialPosition) < 0.5f;
        if (justGrounded)
        {
            if (curTimeBtwJump > timeBtwJump)
            {
                if (!inStartPos)
                {
                    transform.position = Vector3.MoveTowards(transform.position, initialPosition, translationSpeed * Time.deltaTime);
                }
                else
                {
                    justGrounded = false;
                    curTimeBtwJump = 0;
                    //transform.position = initialPosition;  
                }
                
            }
            else
            {
                curTimeBtwJump += Time.deltaTime;
            }
        }
        else if (groundChecker.isGrounded)
        {
            if ((player.GetPosition().x < transform.position.x && transform.rotation.y == 0)
            ||  (player.GetPosition().x > transform.position.x && transform.rotation.y != 0))
            {
                transform.eulerAngles = new Vector3(0, transform.rotation.y == 0? 180:0);
            }
        }


        if (Vector2.Distance(player.GetPosition(), accessMinigame.transform.position) <= accessMinigame.radius)
        {
            if (accessMinigame.available)
            {
                if (spriteRenderer.color != interactableColor) spriteRenderer.color = interactableColor;
            }
            else
            {
                if (spriteRenderer.color != notInteractColor) spriteRenderer.color = notInteractColor;
            }
        }
        else
        {
            if (spriteRenderer.color != defaultColor) spriteRenderer.color = defaultColor;
        }

        
    }

    void collisionHandler_TouchingContact(GameObject contact)
    {
        //rb.centerOfMass = partsHandler.Center;
        if (contact.tag == "Player")
        {
            if (!justGrounded && isReference && groundChecker.isGrounded)
            {
                Debug.Log("should jump");
                Vector2 jumpDirection = new Vector2
                (
                    player.GetPosition().x > transform.position.x ?
                    jumpForce.x : -jumpForce.x, 
                    jumpForce.y
                );
                rb.AddForce(jumpDirection, ForceMode2D.Impulse);
            }
        }
        
    }

    void groundChecker_Grounded(string groundTag)
    {
        justGrounded = true; 
        rb.velocity = new Vector2();
    }

    void partsHandler_ChangedReference(GameObject reference)
    {
        if (positionReference.Equals(reference))
        {
            isReference = true;
            GetComponent<SpriteRenderer>().enabled = true;
            machineFx.SetActive(true);
            transform.position = initialPosition;

        }
        else
        {
            isReference = false;
            GetComponent<SpriteRenderer>().enabled = false;
            machineFx.SetActive(false);

            accessMinigame = null;
        }
    }

}

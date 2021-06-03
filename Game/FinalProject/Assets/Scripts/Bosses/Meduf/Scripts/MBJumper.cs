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
    #endregion

    #region Rigid, Colliders
    [Header("Rigid, Colliders")]
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private GroundChecker groundChecker;
    private Rigidbody2D rb;
    private bool justGrounded;
    #endregion

    #region References
    [Header("References")]
    [SerializeField] private MBPartsHandler partsHandler;
    [SerializeField] private GameObject positionReference;
    private PlayerManager player;
    private bool isReference;
    #endregion    


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;

        rb = GetComponent<Rigidbody2D>();
        //partsHandler = GetComponent<MBPartsHandler>();
    
        collisionHandler.StayTouchingContactHandler += collisionHandler_TouchingContact;
        groundChecker.GroundedHandler += groundChecker_Grounded;
        partsHandler.ChangedReferenceHandler += partsHandler_ChangedReference;
    }

    // Update is called once per frame
    void Update()
    {
        if (justGrounded)
        {
            if (curTimeBtwJump > timeBtwJump)
            {
                justGrounded = false;
                curTimeBtwJump = 0;
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
                Debug.Log("swaaaa");
                transform.eulerAngles = new Vector3(0, transform.rotation.y == 0? 180:0);
            }
        }

        
    }

    void collisionHandler_TouchingContact(GameObject contact)
    {
        //rb.centerOfMass = partsHandler.Center;
        if (contact.tag == "Player")
        {
            if (!justGrounded && isReference && groundChecker.isGrounded)
            {
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
        }
        else
        {
            isReference = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}

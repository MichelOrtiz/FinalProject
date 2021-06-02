using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform feetPos;
    [SerializeField] public float checkFeetRadius;
    //private BoxCollider2D collider2D;
    public bool isGrounded;
    public string lastGroundTag;

    public delegate void ChangedGroundTag();
    public event ChangedGroundTag ChangedGroundTagHandler;
    protected virtual void OnChangedGroundTag()
    {
        ChangedGroundTagHandler?.Invoke();
    }

    public delegate void Grounded(string groundTag);
    public delegate void GroundedGameObject(GameObject ground);

    public event Grounded GroundedHandler;
    public event GroundedGameObject GroundedGameObjectHandler;
    protected virtual void OnGrounded(string groundTag)
    {
        GroundedHandler?.Invoke(groundTag);
    }
    protected virtual void OnGroundedGameObject(GameObject ground)
    {
        GroundedGameObjectHandler?.Invoke(ground);
    }

    public delegate void ExitGround();
    public event ExitGround ExitGroundHandlder;
    protected virtual void OnExitGround()
    {
        ExitGroundHandlder?.Invoke();
    }

    void Start()
    {
        //feetPos = PlayerManager.instance.feetPos;
        //collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform")
        {
            OnGrounded(other.gameObject.tag);
            if (other.gameObject.tag != lastGroundTag)
            {
                lastGroundTag = other.gameObject.tag;
                OnChangedGroundTag();
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (!isGrounded)
        {
            OnExitGround();
        }
    }

}

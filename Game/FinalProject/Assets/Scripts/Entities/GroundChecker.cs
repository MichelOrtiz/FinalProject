using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    #region Ground And Edge Variables
    [Header("Ground")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform feetPos;
    [SerializeField] public float checkFeetRadius;
    public bool isGrounded;
    public string lastGroundTag;

    [Header("Edge")]
    [SerializeField] private Transform edgeCheck;
    [SerializeField] private float downCheckDistance;
    public bool isNearEdge;
    #endregion

    #region Tags
    public static List<string> GroundTags = new List<string>{ "Ground", "Platform",  "Ice"};
    #endregion

    #region Events
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

    public delegate void NearEdge();
    public event NearEdge NearEdgeHandler;
    protected virtual void OnNearEdge()
    {
        NearEdgeHandler?.Invoke();
    }
    #endregion

    void Start()
    {
        //feetPos = PlayerManager.instance.feetPos;
        //collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkFeetRadius, whatIsGround);
        isNearEdge = IsNearEdge();
        if (isNearEdge)
        {
            OnNearEdge();
        }
    }

    public bool IsNearEdge()
    {
        //bool nearEdge;
        try
        {
            return !Physics2D.OverlapArea(edgeCheck.position, ((Vector2)edgeCheck.position + Vector2.down * downCheckDistance), whatIsGround); //(Physics2D.Raycast(edgeCheck.position, Vector3.down, downCheckDistance)).collider.IsTouchingLayers(whatIsGround);
        }
        catch (System.NullReferenceException)
        {
            return true;
        }
    }
    public bool IsNearEdge(string groundTag)
    {
        //bool nearEdge;
        try
        {
            return Physics2D.OverlapArea(edgeCheck.position, ((Vector2)edgeCheck.position + Vector2.down * downCheckDistance), whatIsGround).gameObject.tag != groundTag; //(Physics2D.Raycast(edgeCheck.position, Vector3.down, downCheckDistance)).collider.IsTouchingLayers(whatIsGround);
        }
        catch (System.NullReferenceException)
        {
            return true;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.gameObject.tag;
        if (GroundTags.Contains(tag))
        {
            OnGrounded(tag);
            if (tag != lastGroundTag)
            {
                lastGroundTag = tag;
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

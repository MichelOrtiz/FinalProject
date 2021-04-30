using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkFeetRadius;
    //private BoxCollider2D collider2D;
    public bool isGrounded;
    public string lastGroundTag;

    public delegate void ChangedGroundTag();
    public event ChangedGroundTag ChangedGroundTagHandler;
    protected virtual void OnChangedGroundTag()
    {
        ChangedGroundTagHandler?.Invoke();
    }

    void Start()
    {
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
            if (other.gameObject.tag != lastGroundTag)
            {
                lastGroundTag = other.gameObject.tag;
                OnChangedGroundTag();
            }
        }
    }
}

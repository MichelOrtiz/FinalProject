using UnityEngine;
using System.Collections.Generic;
public class CollisionHandler : MonoBehaviour
{
    //private Collider2D collider;
    //[SerializeField] private List<string> contacts;
    //public List<string> Contacts { get; private set; }

    [SerializeField] private List<GameObject> contacts;
    public List<GameObject> Contacts
    {
        get { return contacts; }
        protected set { contacts = value; }
    }

    public delegate void EnterTouchingContact(GameObject contactTag);
    public event EnterTouchingContact EnterTouchingContactHandler;
    protected virtual void OnEnterTouchingContact(GameObject tag)
    {
        EnterTouchingContactHandler?.Invoke(tag);
    }

    public delegate void StayTouchingContact(GameObject contactTag);
    public event StayTouchingContact StayTouchingContactHandler;
    protected virtual void OnStayTouchingContact(GameObject tag)
    {
        StayTouchingContactHandler?.Invoke(tag);
    }

    public delegate void ExitTouchingContact(GameObject contactTag);
    public event ExitTouchingContact ExitTouchingContactHandler;
    protected virtual void OnExitTouchingContact(GameObject tag)
    {
        ExitTouchingContactHandler?.Invoke(tag);
    }

    void Awake()
    {
        Contacts = new List<GameObject>();
    }
    void Start()
    {
         
    }

    void Update()
    {
     
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnEnterTouchingContact(other.gameObject);
        if (!Contacts.Contains(other.gameObject))
        {
            Contacts.Add(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnStayTouchingContact(other.gameObject);
    }

    
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        OnExitTouchingContact(other.gameObject);
        if (Contacts.Contains(other.gameObject))
        {
            Contacts.Remove(other.gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        OnEnterTouchingContact(other.gameObject);
    }
    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        OnStayTouchingContact(other.gameObject);
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        OnExitTouchingContact(other.gameObject);
    }
}
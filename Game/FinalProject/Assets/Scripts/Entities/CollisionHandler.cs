using UnityEngine;
using System.Collections.Generic;
public class CollisionHandler : MonoBehaviour
{
    //private Collider2D collider;
    //[SerializeField] private List<string> contacts;
    //public List<string> Contacts { get; private set; }
    public string lastColliderTag;

    [SerializeField] private List<GameObject> contacts;
    public List<GameObject> Contacts
    {
        get { return contacts; }
        protected set { contacts = value; }
    }

    public delegate void EnterTouchingContact(GameObject contact);
    public event EnterTouchingContact EnterTouchingContactHandler;
    protected virtual void OnEnterTouchingContact(GameObject contact)
    {
        EnterTouchingContactHandler?.Invoke(contact);
    }

    public delegate void StayTouchingContact(GameObject contact);
    public event StayTouchingContact StayTouchingContactHandler;
    protected virtual void OnStayTouchingContact(GameObject contact)
    {
        StayTouchingContactHandler?.Invoke(contact);
    }

    public delegate void ExitTouchingContact(GameObject contact);
    public event ExitTouchingContact ExitTouchingContactHandler;
    protected virtual void OnExitTouchingContact(GameObject contact)
    {
        ExitTouchingContactHandler?.Invoke(contact);
    }
    public delegate void ChangedColliderTag();
    public event ChangedColliderTag ChangedColliderTagHandler;
    protected virtual void OnChangedColliderTag()
    {
        ChangedColliderTagHandler?.Invoke();
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

    public bool TouchingContact(GameObject contact)
    {
        return Contacts.Contains(contact);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnEnterTouchingContact(other.gameObject);
        if (!Contacts.Contains(other.gameObject))
        {
            Contacts.Add(other.gameObject);
        }
        if (other.gameObject.tag != "Untagged" && other.gameObject.tag != lastColliderTag)
            {
                lastColliderTag = other.gameObject.tag;
                OnChangedColliderTag();
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
        if (other.gameObject.tag != "Untagged" && other.gameObject.tag != lastColliderTag)
            {
                lastColliderTag = other.gameObject.tag;
                OnChangedColliderTag();
            }
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
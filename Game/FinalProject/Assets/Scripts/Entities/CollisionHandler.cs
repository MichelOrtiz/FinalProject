using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : MonoBehaviour
{
    //private Collider2D collider;
    //[SerializeField] private List<string> contacts;
    //public List<string> Contacts { get; private set; }
    public string lastColliderTag;
    public Collider2D Collider2D { get => GetComponent<Collider2D>(); }


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
        if (Contacts != null)
        {
            return Contacts.Contains(contact);
        }
        return false;
    }

    public bool TouchingContact(string tag)
    {
        if (Contacts != null)
        {
            return Contacts.Exists(c => c.tag == tag);
        }
        return false;
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

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (!Contacts.Contains(other.gameObject))
        {
            Contacts.Add(other.gameObject);
        }
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
    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        if (!Contacts.Contains(other.gameObject))
        {
            Contacts.Add(other.gameObject);
        }
        OnStayTouchingContact(other.gameObject);
    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        OnExitTouchingContact(other.gameObject);
        if (Contacts.Contains(other.gameObject))
        {
            Contacts.Remove(other.gameObject);
        }
    }
}
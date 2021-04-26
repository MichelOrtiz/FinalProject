using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBounds : MonoBehaviour
{

    public delegate void EnteredBounds();
    public event EnteredBounds EnteredBoundsHandler;

    void OnEnteredBounds()
    {
        EnteredBoundsHandler?.Invoke();
    }

    public delegate void ExitBounds();
    public event ExitBounds ExitBoundsHandler;
    void OnExitBounds()
    {
        ExitBoundsHandler?.Invoke();
    }

    public delegate void StayInBounds();
    public event StayInBounds StayInBoundsHandler;
    void OnStayInBounds()
    {
        StayInBoundsHandler?.Invoke();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Enemy")
        {
            Debug.Log(other.gameObject.name + " inside");
            other.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }*/
        Debug.Log(other.gameObject.GetComponent<Entity>());

        if (other.GetComponent<Entity>() &&
            other.GetComponent<Entity>() is IBattleBounds)
        {
            Debug.Log(other + "Entered" + gameObject);
            OnEnteredBounds();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.layer = LayerMask.NameToLayer("Ghost");
        }*/
        if (other.GetComponent<Entity>() &&
            other.GetComponent<Entity>() is IBattleBounds)
        {
            OnStayInBounds();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.layer = LayerMask.NameToLayer("Enemies");
        }*/
        if (other.GetComponent<Entity>() && 
            other.GetComponent<Entity>() is IBattleBounds)
        {
            OnExitBounds();
        }
    }
}

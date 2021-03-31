using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    [SerializeField] private float radius;
    private GameObject colliderObject;
    void Start()
    {
        //entity = FungusBoss.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool NearEdge(Entity entity)
    {
        try
        {
            colliderObject.GetComponent<Entity>();
        }
        catch (System.Exception)
        {
            return false;
        }
        Debug.Log(colliderObject.GetComponent<Entity>() == entity);
        return colliderObject.GetComponent<Entity>() == entity;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        colliderObject = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colliderObject = null;
    }
}

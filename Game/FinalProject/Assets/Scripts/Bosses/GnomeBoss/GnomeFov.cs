using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GnomeFov : MonoBehaviour
{
    [SerializeField] protected float speedMultiplier;
    //[SerializeField] private float interval;
    [SerializeField] protected List<Vector2> positions;
    [SerializeField] protected Transform groundCheck;
    protected bool touchingPlayer;
    //protected float speed;
    int index = 0;
    //private float currentTime;

    protected abstract void Move();

    // Start is called before the first frame update
    protected void Start()
    {
        //speed = speed * Entity.averageSpeed;
        /*Mesh mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;*/
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!IsNearEdge())
        {
            Move();
        }
        else
        {
            ChangePosition();
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            Debug.Log("Fov touching player");
        }

    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            Debug.Log("Fov stopped touching player");
        }
    }

    protected void ChangePosition()
    {
        transform.position = positions[index];

        if (index < positions.Count-1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

    protected bool IsNearEdge()
    {
        return !(Physics2D.Raycast(groundCheck.position, Vector3.down, 1f)).collider;
    }
}
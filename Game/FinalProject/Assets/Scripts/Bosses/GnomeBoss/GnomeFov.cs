using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeFov : MonoBehaviour
{
    [SerializeField] private float interval;
    [SerializeField] private List<Vector2> positions;
    [SerializeField] private Transform groundCheck;
    int index = 0;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        /*Mesh mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;*/
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("is near edge: " + IsNearEdge());
        if (currentTime <= 0)
        {
            ChangePosition();
            currentTime = interval;
        }
        else
        {
            currentTime -= Time.deltaTime; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Fov touching player");
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Fov stopped touching player");
        }
    }

    void ChangePosition()
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
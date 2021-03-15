using UnityEngine;

public class ProjectileWarning : MonoBehaviour
{
    [SerializeField] private float viewTime;
    private Vector3 position;

    void Start()
    {
        //transform.position = position;
    }

    void Update()
    {
        if (viewTime > 0)
        {
            viewTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
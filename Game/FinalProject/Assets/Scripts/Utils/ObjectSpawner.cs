using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private int numberOfSpawns;

    [SerializeField] private float timeBeforeSpawn;
    private float currentTime;
    [SerializeField] private bool loop;
    /// <summary>
    /// Center position of the bounds:
    /// <see langword="false"/> to use parent position, 
    /// <see langword="true"/> to use world custom position
    /// </summary>
    [SerializeField] private bool useWorldPosition;

    // Only if false in useParentPosition
    [SerializeField] private Vector2 worldPosition;

    [SerializeField] private BoxCollider2D box;

    void Awake()
    {
        if (box == null)
        {
            box = GetComponent<BoxCollider2D>();
        }

        if (useWorldPosition)
        {
            box.transform.position = worldPosition;
        }
    }

    void Start()
    {
        //SpawnObjects();
    }


    void Update()
    {
        if (currentTime >= timeBeforeSpawn)
        {
            SpawnObjects();
            currentTime = -0.1f;
        }
        else if (loop)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void SpawnObjects()
    {
        int index = 0;
        while (++index < numberOfSpawns)
        {
            Vector2 position = RandomGenerator.RandomPointInBounds(box.bounds);
            Instantiate(objectToInstantiate, position, objectToInstantiate.transform.rotation);

        }
    }

}
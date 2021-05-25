using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private int numberOfSpawns;

    [SerializeField] private float timeBeforeSpawn;
    private float currentTime;
    [SerializeField] private bool loop;


    private BoxCollider2D box;


    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        //SpawnObjects();
    }


    void Update()
    {
        if (currentTime >= timeBeforeSpawn)
        {
            SpawnObjects();
            currentTime = -1;
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
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private int numberOfSpawns;

    private BoxCollider2D box;


    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        SpawnObjects();
    }


    void Update()
    {
        
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
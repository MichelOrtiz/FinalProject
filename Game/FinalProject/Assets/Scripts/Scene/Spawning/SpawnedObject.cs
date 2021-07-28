using UnityEngine;
[System.Serializable]
public class SpawnedObject
{
    public GameObject gameObject;
    public Vector2 spawnedPos;

    public SpawnedObject(GameObject gameObject, Vector2 spawnedPos)
    {
        this.gameObject = gameObject;
        this.spawnedPos = spawnedPos;
    }
}
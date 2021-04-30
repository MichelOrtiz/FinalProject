using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Stage", menuName = "Stage/new Stage")]
public class Stage : ScriptableObject
{
    //[SerializeField]private List<GameObject> stageObject;
    //[SerializeField] private SerializableDictionary<Vector3, GameObject> stageObjects;
    
    [SerializeField] public List<GameObject> gameObjects;
    [SerializeField] public List<Vector2> positions;
    public List<GameObject> currentObjects { get; set; }
    public void Generate(){
        for (int i = 0; i < gameObjects.Count; i++)
        {
            currentObjects.Add(Instantiate(gameObjects[i], positions[i], gameObjects[i].transform.rotation));
        }
    }
    public void Destroy(){
        foreach (GameObject obj in currentObjects)
        {
            if(obj!=null)
            Destroy(obj);
        }
    }
}
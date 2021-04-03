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
    private List<GameObject> currentObjects;
    public void Generate(){
        for (int i = 0; i < gameObjects.Count; i++)
        {
            currentObjects.Add(Instantiate(gameObjects[i], positions[i],Quaternion.identity));
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
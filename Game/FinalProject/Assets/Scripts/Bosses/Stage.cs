using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Stage", menuName = "Stage/new Stage")]
public class Stage : ScriptableObject
{
    [SerializeField]private List<GameObject> stageObject;
    private List<GameObject> currentObjects;
    public void Generate(){
        foreach(GameObject obj in stageObject){
            currentObjects.Add(Instantiate(obj,PlayerManager.instance.GetPosition(),Quaternion.identity));
            
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

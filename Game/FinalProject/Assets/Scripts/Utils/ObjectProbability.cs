using System;
using UnityEngine;

[Serializable]
public class ObjectProbability<T>
{
    [SerializeField] private T tObject;
    public T TObject { get => tObject; }
    [Range(0,100)]
    [SerializeField] private float probability;
    public float Probability { get => probability; }


    readonly float minProbability = 0.0001f;

    public ObjectProbability(T tObject, float probability)
    {
        this.tObject = tObject;
        //if (probability == 0 || probability < minProbability) probability = minProbability; 
        this.probability = probability;
    }
}
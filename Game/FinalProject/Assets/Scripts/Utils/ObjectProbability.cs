using System;
using UnityEngine;

[Serializable]
public class ObjectProbability<T>
{
    [SerializeField] private T tObject;
    public T TObject { get => tObject; }
    [SerializeField] private float probability;
    public float Probability { get => probability; }


    public ObjectProbability(T tObject, float probability)
    {
        this.tObject = tObject;
        this.probability = probability;
    }
}
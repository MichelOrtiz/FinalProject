using UnityEngine;
using System;

[RequireComponent (typeof(Rigidbody2D))] 
public class FollowerObject : MonoBehaviour
{
    public enum FollowerType
    {
        Spore
    }

    public FollowerType type;
    //public GameObject follower;
    public GameObject target;
    public float angle;
    public float distance;

    /// <summary>
    ///  Lifetime of the object. 0 for no lifetime (never destroy).
    /// </summary>
    public float lifeTime;
    private float curTime;

    public FollowerObject(float angle, float distance, float lifeTime)
    {
        //this.follower = follower;
        this.angle = angle;
        this.distance = distance;
        this.lifeTime = lifeTime;
    }
    

    void Update()
    {
        if (lifeTime != 0)
        {
            if (curTime > lifeTime)
            {
                curTime = 0;
                Destroy(gameObject);
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }
    }
}
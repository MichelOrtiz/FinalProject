using UnityEngine;
using System.Collections;
public class Reflex : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float delay;
    private float curTime;
    private Queue positions;
    private Vector3 currentTarget;
    //private Transform currentTarget;
    [SerializeField] private float nodeDistance;

    /*class Cloner
    {
        public Vector2 target;
        
    }*/


    new void Awake()
    {
        base.Awake();
        positions = new Queue();
        currentTarget = GetPosition();
        //currentTarget = transform;
    }

    new void Start()
    {
        base.Start();
        positions.Enqueue(player.GetPosition());
    }


    new void Update()
    {
        if ( positions.Count == 0 || Vector2.Distance(player.GetPosition(), (Vector3)positions.Peek()) >= nodeDistance)
        {
            positions.Enqueue(player.GetPosition());
        }
        base.Update();
    }
    
    new void FixedUpdate()
    {
        if (curTime > delay)
        {
            enemyMovement.GoTo(currentTarget, chasing: true, gravity: false);

            if (GetPosition() == currentTarget)
            {
                if (positions.Count > 0)
                {
                    currentTarget = (Vector3)positions.Dequeue();
                }
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
        base.FixedUpdate();
    }

    
}
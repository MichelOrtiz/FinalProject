using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBPartsHandler : MonoBehaviour
{
    #region Part Stuff
    [Header("Part Stuff")]
    [SerializeField] private List<GameObject> parts;
    private int totalParts;
    [SerializeReference] private List<GameObject> movedParts; 
    #endregion

    #region GameObject Position Reference
    [Header("GameObject position reference")]
    [SerializeField] private GameObject rightPositionReference;
    [SerializeField] private GameObject leftPositionReference;

    [SerializeReference] private GameObject currentPositionsReference;
    #endregion

    #region Speed and Time
    [Header("Speed and Time")]
    [SerializeField] private float speedMultiplier;
    private float speed;
    [SerializeField] private float timeBtwMove;
    private float curTimeBtwMove;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        speed = Entity.averageSpeed * speedMultiplier;

        totalParts = parts.Count + movedParts.Count;
        currentPositionsReference = rightPositionReference;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (curTimeBtwMove > timeBtwMove)
        {
            AddMovedPart();
            curTimeBtwMove = 0;
        }
        else
        {
            curTimeBtwMove += Time.deltaTime;
        }

        if (movedParts.Count == totalParts)
        {

        }

    }

    void FixedUpdate()
    {
        foreach (var part in movedParts)
        {
            Transform objectReference = ScenesManagers
                .GetComponentsInChildrenList<Transform>(GetTargetPositionReference())
                .Find(g => g.gameObject.ToString() == part.gameObject.ToString());

            Debug.Log( $"objectReference null? {objectReference == null}" );

            //Vector2 pairPosition = 

            if (objectReference != null)
            {
                part.transform.position = Vector2.MoveTowards(part.transform.position, objectReference.transform.position, speed * Time.deltaTime);
            }
        }
    }

    void AddMovedPart()
    {
        try
        {
            int randomPart = 0;
            randomPart = RandomGenerator.NewRandom(0, parts.Count-1);

            movedParts.Add(parts[randomPart]);
            parts.RemoveAt(randomPart);
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }

    private GameObject GetTargetPositionReference()
    {
        if (currentPositionsReference == rightPositionReference)
        {
            return leftPositionReference;
        }
        else
        {
            return rightPositionReference;
        }
    }
}

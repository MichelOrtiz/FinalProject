using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBPartsHandler : MonoBehaviour, ILaser
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

    private Vector2 lastPartPosition;
    [SerializeField] private float yCheckRange;

    #endregion

    #region Speed and Time
    [Header("Speed and Time")]
    [SerializeField] private float speedMultiplier;
    private float speed;
    [SerializeField] private float timeBtwMove;
    private float curTimeBtwMove;

    [SerializeField] private float waitTimeWhenAssembled;
    private float curAssembledTime;
    private bool assembled;

    
    #endregion

    #region LaserWarning
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;

    public Transform ShotPos {get; set;}

    public Vector2 EndPos {get; set;}


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        speed = Entity.averageSpeed * speedMultiplier;

        totalParts = parts.Count + movedParts.Count;
        currentPositionsReference = leftPositionReference;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Assembled())
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
        }
        else
        {
            if (parts.Count != movedParts.Count)
            {
                parts.AddRange(movedParts);
            }
            if (curAssembledTime > waitTimeWhenAssembled)
            {
                //assembled = false;
                movedParts.Clear();
                ChangePositionReference();
                curAssembledTime = 0;
            }
            else
            {
                curAssembledTime += Time.deltaTime;
            }
        }
    }

    

    void FixedUpdate()
    {
        foreach (var part in movedParts)
        {
            /*Transform objectReference = ScenesManagers
                .GetComponentsInChildrenList<Transform>(GetTargetPositionReference())
                .Find(g => g.gameObject.ToString() == part.gameObject.ToString());*/

            var positionReference = GetTargetPosition(part);

            if (positionReference != null)
            {
                part.transform.position = Vector2.MoveTowards(part.transform.position, positionReference, speed * Time.deltaTime);
                
                
            }
        }
    }

    void AddMovedPart()
    {
        try
        {
            int randomPart = 0;
            randomPart = RandomGenerator.NewRandom(0, parts.Count-1);

            /*do
            {
                Debug.Log("imindowhile");
                randomPart = RandomGenerator.NewRandom(0, parts.Count-1);

                if (movedParts.Count == 0)
                {
                    Debug.Log("should break loop");
                    break;
                }
                else
                {
                    distance = Math.Abs( parts[randomPart].transform.position.y - lastPartPosition.y); 
                }
            }
            while ( distance <= yCheckRange );
            Debug.Log("imoutoftheloop");
            lastPartPosition = movedParts[randomPart].transform.position;*/
            movedParts.Add(parts[randomPart]);
            ShotPos = parts[randomPart].transform;
                EndPos = GetTargetPosition(parts[randomPart]);

                ShootLaser(ShotPos.position, EndPos);
            parts.RemoveAt(randomPart);
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }

    private Vector2 GetTargetPosition(GameObject part)
    {
        Vector2 position = ScenesManagers
                .GetComponentsInChildrenList<Transform>(currentPositionsReference)
                .Find(g => g.gameObject.ToString() == part.gameObject.ToString()).position;

        if (position != null)
        {
            return position;
        }
        else
        {
            return new Vector2();
        }
    }

    private void ChangePositionReference()
    {
        currentPositionsReference = 
            currentPositionsReference == rightPositionReference? 
                leftPositionReference :
                rightPositionReference;
    }

    /*private GameObject GetTargetPositionReference()
    {
        if (currentPositionsReference == rightPositionReference)
        {
            return leftPositionReference;
        }
        else
        {
            return rightPositionReference;
        }
    }*/


    bool Assembled()
    {
        if (movedParts.Count < totalParts)
        {
            return false;
        }
        
        foreach (var part in movedParts)
        {
            if ((Vector2)part.transform.position != GetTargetPosition(part))
            {
                return false;
            }
        }
        return true;
    }

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, this);
    }

    public void LaserAttack()
    {
        //throw new NotImplementedException();
    }
}

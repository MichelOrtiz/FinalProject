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
    //[SerializeReference] private List<GameObject> assembledParts;
    #endregion

    #region GameObject Position Reference
    [Header("GameObject position reference")]
    [SerializeField] private GameObject rightPositionReference;
    [SerializeField] private GameObject leftPositionReference;
    [SerializeReference] private GameObject currentPositionsReference;

    private Vector2 lastPartPosition;
    [SerializeField] private float yCheckRange;

    public Vector2 Center { get; set; }

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

    #region Events
    /*public delegate void HalfPartsReached(Vector2 center);
    public event HalfPartsReached HalfPartsReachedHandler;
    protected virtual void OnHalfPartsReached(Vector2 center)
    {
        Debug.Log(center);
        assembledHalf = true;
        HalfPartsReachedHandler?.Invoke(center);
    }
    private bool assembledHalf;*/

    public delegate void ChangedReference(GameObject reference);
    public event ChangedReference ChangedReferenceHandler;
    protected virtual void OnChangedReference(GameObject reference)
    {
        ChangedReferenceHandler?.Invoke(reference);
    }
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        speed = Entity.averageSpeed * speedMultiplier;

        totalParts = parts.Count + movedParts.Count;
        currentPositionsReference = leftPositionReference;
        OnChangedReference(currentPositionsReference);

        Center = MathUtils.FindCenterOfTransforms(parts.GetRange(0, parts.Count));

    }

    // Update is called once per frame
    void Update()
    {
        //if (!Assembled(totalParts))
        if (!Assembled(totalParts))
        {
            if (curTimeBtwMove > timeBtwMove)
            {
                AddMovedPart();
                curTimeBtwMove = 0;
            }
            else
            {
                //UpdateAssembledParts();
                curTimeBtwMove += Time.deltaTime;
            }

            /*if (assembledParts.Count >= totalParts/2)
            {
                //if (!assembledHalf)
                {
                    Center = MathUtils.FindCenterOfTransforms(assembledParts.GetRange(0, assembledParts.Count));
                    //OnHalfPartsReached(center);
                }
            }*/
            /*else
            {
                assembledHalf = false;
            }*/
        }
        else
        {
            if (parts.Count == 0)
            {
                parts.AddRange(movedParts);
                //Center =  MathUtils.FindCenterOfTransforms(assembledParts.GetRange(0, parts.Count));
            }
            if (curAssembledTime > waitTimeWhenAssembled)
            {
                //assembled = false;
                //movedParts.Clear();
                movedParts.Clear();
                ChangePositionReference();
                OnChangedReference(currentPositionsReference);
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

            var reference = GetTargetReference(part);

            if (reference != null)
            {
                part.transform.position = Vector2.MoveTowards(part.transform.position, reference.transform.position, speed * Time.deltaTime);
                part.transform.rotation = reference.transform.rotation;
                
            }
        }
    }

    void AddMovedPart()
    {
        try
        {
            int randomPart = 0;
            randomPart = RandomGenerator.NewRandom(0, parts.Count-1);
            ShotPos = parts[randomPart].transform;
            EndPos = GetTargetReference(parts[randomPart]).transform.position;

            ShootLaser(ShotPos.position, EndPos);

            if (!movedParts.Contains(parts[randomPart]))
            {
                movedParts.Add(parts[randomPart]);
                parts.RemoveAt(randomPart);
            }
            
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }

        
    }


    private GameObject GetTargetReference(GameObject part)
    {
        /*Vector2 position = ScenesManagers
                .GetComponentsInChildrenList<Transform>(currentPositionsReference)
                .Find(g => g.gameObject.ToString() == part.gameObject.ToString()).position;*/

        GameObject reference = ScenesManagers
                .GetComponentsInChildrenList<Transform>(currentPositionsReference)
                .Find(g => g.gameObject.ToString() == part.gameObject.ToString()).gameObject;

        if (reference != null)
        {
            return reference;
        }
        else
        {
            return new GameObject();
        }
    }

    private void ChangePositionReference()
    {
        currentPositionsReference = 
            currentPositionsReference == rightPositionReference? 
                leftPositionReference :
                rightPositionReference;
    }


    bool Assembled(int manyParts)
    {
        if (movedParts.Count < manyParts)
        {
            return false;
        }
        
        /*List<GameObject> someParts = new List<GameObject>();
        someParts.AddRange()*/

        foreach (var part in movedParts.GetRange(0, manyParts))
        {
            if (part.transform.position != GetTargetReference(part).transform.position)
            {
                return false;
            }
        }
        return true;
    }

    /*void UpdateAssembledParts()
    {
        assembledParts.AddRange(movedParts.FindAll(p => (Vector2)p.transform.position == GetTargetPosition(p)));
        //movedParts.RemoveAll(p => (Vector2)p.transform.position == GetTargetPosition(p));
    }*/


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

using UnityEngine;
using System.Collections.Generic;
public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer ray;
    [SerializeField] private Transform endPoint;
    [SerializeField] private bool hasLifeTime;
    [SerializeField] private float lifeTime;

    [SerializeField] protected bool targetWarningAvailable;
    [SerializeField] private GameObject warning;
    private GameObject warningObject;
    private LineRenderer warningLine;
    [SerializeField] private float warningTime;
    private float currentWarningTime;


    private float currentTime;
    [SerializeField] private float speed;
    [SerializeField] private float secondSpeed;
    [SerializeField] private bool chaseTargetPosition;
    [SerializeField] private bool chaseOnReachedEndPos;
    private bool reachedEndPos;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;
    
    private bool touchingPlayer;
    private ILaser summoner;
    private PlayerManager player;


    private EdgeCollider2D edge;


    //public delegate void ReachedEndPos();
    //public event ReachedEndPos ReachedDestinationHandler;

    public Laser(){}

    /*public Laser(Vector2 startPos, Vector2 endPos, float speed, ILaser summoner)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;
        this.summoner = summoner;

    }*/

    public void Setup(Vector2 startPos, Vector2 endPos, float speed, ILaser summoner)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.speed = speed;
        this.summoner = summoner;
        direction = (endPos - startPos).normalized;


        if (targetWarningAvailable)
        {
            /*try
            {
                warningLine = warning.GetComponent<LineRenderer>();
                warningLine.SetPosition(0, startPos);
                warningLine.SetPosition(1, endPos);

                Debug.Log("Start:" + warningLine.GetPosition(0));
                Debug.Log("End:" + warningLine.GetPosition(1));
            }
            catch (System.NullReferenceException ex)
            {
                Debug.Log("ERROR: No GameObject assigned to warning: " + ex.Message);
            }*/
            warningObject = Instantiate(warning, endPos, Quaternion.identity);
        }
    }

    public void Setup(Vector2 startPos, Vector2 endPos, ILaser summoner)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.summoner = summoner;
        direction = (endPos - startPos).normalized;


        if (targetWarningAvailable)
        {
            warningObject = Instantiate(warning, endPos, Quaternion.identity);
        }
    }

    void Awake()
    {
        if (chaseTargetPosition && chaseOnReachedEndPos)
        {
              chaseTargetPosition = !chaseOnReachedEndPos;
        }
    }

    void Start()
    {
        player = PlayerManager.instance;
        ray.SetPosition(0, startPos);
        ray.SetPosition(1, startPos);

        edge = GetComponent<EdgeCollider2D>();

        //chaseOnReachedEndPos = chaseOnReachedEndPos && chaseTargetPosition;

        
    }
    void Update()
    {
        /*if (chaseTargetPosition)
        {
            endPos = summoner.EndPos;
            direction = (endPos- startPos).normalized;
        }*/

        if (targetWarningAvailable)
        {
            if (currentWarningTime > warningTime)
            {
                Destroy(warningObject);
                targetWarningAvailable = false;
            }
            else
            {
                currentWarningTime += Time.deltaTime;
            }
        }
        else
        {
            if (hasLifeTime && currentTime > lifeTime)
            {
                Destroy(gameObject);
            }
            else
            {
                currentTime += Time.deltaTime;
            }

            
            
            edge.SetPoints(new List<Vector2>()
                {transform.InverseTransformPoint(startPos) , transform.InverseTransformPoint(endPoint.position)}
            );

            if (touchingPlayer)
            {
                summoner.LaserAttack();
                touchingPlayer = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (summoner?.ShotPos != null)
        {
            ExtendRay();
        }
    }
 
    void ExtendRay()
    {
        //endPoint.transform.position = Vector2.MoveTowards(startPos, endPos, speed * Time.deltaTime);
        ray.SetPosition(0, summoner.ShotPos.position);
        float distanceToEndPos = Vector2.Distance(endPos, startPos);
        float currentDistanceFromStart = Vector2.Distance(endPoint.position, startPos);

        if (chaseTargetPosition || reachedEndPos)
        {
            endPoint.transform.position = Vector2.MoveTowards(endPoint.transform.position, summoner.EndPos, secondSpeed * Time.deltaTime);
        }
        else if (currentDistanceFromStart < distanceToEndPos)
        {
            endPoint.transform.position += (Vector3) (direction * speed * Time.deltaTime);
        }

        if (currentDistanceFromStart >= distanceToEndPos)
        {
            OnReachedEndPos();
        }

        ray.SetPosition(1, endPoint.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    public void OnReachedEndPos()
    {
        reachedEndPos = true;
        /*if (chaseOnReachedEndPos)
        {
            endPoint.transform.position = Vector2.MoveTowards(endPoint.transform.position, summoner.EndPos, speed * Time.deltaTime);
        }*/
    }
        
}
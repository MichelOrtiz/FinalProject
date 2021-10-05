using UnityEngine;
using System.Collections.Generic;
public class Laser : MonoBehaviour
{

    [SerializeField] private LineRenderer ray;
    [SerializeField] private Transform endPoint;
    [SerializeField] private bool hasLifeTime;
    [SerializeField] private float lifeTime;
    private float currentTime;

    public bool targetWarningAvailable;
    [SerializeField] private GameObject warning;
    private GameObject warningObject;
    private LineRenderer warningLine;
    [SerializeField] private float warningTime;
    private float currentWarningTime;


    [SerializeField] private float speed;
    [SerializeField] private float secondSpeed;

    public bool chaseTargetPosition;
    public bool chaseOnReachedEndPos;
    private bool reachedEndPos;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    public bool collidesWithObstacles;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private CollisionHandler collisionHandler;
    [SerializeField] private EdgeCollider2D edge;
    
    private bool touchingPlayer;
    private bool rayHitObstacle;
    private ILaser summoner;
    private PlayerManager player;


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
        if (collisionHandler != null)
        {
            collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterTouchingContactHandler;
            collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitTouchingContactHandler;
        }
    }

    void Start()
    {
        player = PlayerManager.instance;
        ray.SetPosition(0, startPos);
        ray.SetPosition(1, startPos);

        

        if (edge == null)
        {
            edge = GetComponent<EdgeCollider2D>();
        }
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

        try
        {
            rayHitObstacle = FieldOfView.RayHitObstacle(summoner.ShotPos.position, endPoint.position, whatIsObstacle) && collidesWithObstacles;
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex.Message + " -/*/* Probably ok");
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
        startPos = summoner.ShotPos.position;
        ray.SetPosition(0, startPos);
        
        //ray.SetPosition(0, summoner.ShotPos.position);
        float distanceToEndPos = Vector2.Distance(endPos, startPos);
        float currentDistanceFromStart = Vector2.Distance(endPoint.position, startPos);

        if (!rayHitObstacle)
        {
            if (chaseTargetPosition || reachedEndPos)
            {
                endPoint.transform.position = Vector2.MoveTowards(endPoint.transform.position, summoner.EndPos, secondSpeed * Time.deltaTime);
            }
            else if (currentDistanceFromStart < distanceToEndPos)
            {
                endPoint.transform.position += (Vector3) (direction * speed * Time.deltaTime);
            }
        }


        if (currentDistanceFromStart >= distanceToEndPos || rayHitObstacle)
        {
            OnReachedEndPos();
        }

        ray.SetPosition(1, endPoint.position);
    }

    void collisionHandler_EnterTouchingContactHandler(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            touchingPlayer = true;
        }
    }

    void collisionHandler_ExitTouchingContactHandler(GameObject contact)
    {
        if (contact.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
        Debug.Log(other.gameObject.layer);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
        Debug.Log(other.gameObject.layer);
    }
    void OnCollisionExit2D(Collision2D other)
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
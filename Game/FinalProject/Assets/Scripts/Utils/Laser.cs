using UnityEngine;
using System.Collections.Generic;
public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer ray;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float lifeTime;

    [SerializeField] protected bool targetWarningAvailable;
    [SerializeField] private GameObject warning;
    private GameObject warningObject;
    private LineRenderer warningLine;
    [SerializeField] private float warningTime;
    private float currentWarningTime;


    private float currentTime;
    private float speed;

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;
    
    private bool touchingPlayer;
    private ILaser summoner;
    private PlayerManager player;


    private EdgeCollider2D edge;

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


    void Start()
    {
        player = PlayerManager.instance;
        ray.SetPosition(0, startPos);
        ray.SetPosition(1, startPos);

        edge = GetComponent<EdgeCollider2D>();

        
    }
    void Update()
    {
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
            if (currentTime > lifeTime)
            {
                Destroy(gameObject);
            }
            else
            {
                currentTime += Time.deltaTime;
            }


            ExtendRay();
            
            
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
 
    void ExtendRay()
    {
        //endPoint.transform.position = Vector2.MoveTowards(startPos, endPos, speed * Time.deltaTime);
        ray.SetPosition(0, summoner.ShotPos.position);
        float distanceToEndPos = Vector2.Distance(endPos, startPos);
        float currentDistanceFromStart = Vector2.Distance(endPoint.position, startPos);
        if (currentDistanceFromStart < distanceToEndPos)
        {
            endPoint.transform.position += (Vector3) (direction * speed * Time.deltaTime);
            ray.SetPosition(1, endPoint.position);
        }
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
}
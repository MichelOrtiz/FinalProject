using UnityEngine;
using System.Collections.Generic;
public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer ray;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float lifeTime;


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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private BoxCollider2D camBox;
    private GameObject[] boundaries;
    private Bounds[] allBounds;
    private Bounds targetBounds;
    private float waitForSeconds = 0.5f;
    private Vector3 mousePosition;
    public float speed;
    public static CameraFollow instance = null;
    public bool camera1;
    void Awake()
    {
        if (camera1)
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        player = GameObject.Find("Nico-nor").GetComponent<Transform>();
        camBox = GetComponent<BoxCollider2D>();
        //FindLimits();
    }
    void Update()
    { 
        if (camera1)
        {
            //transform.position = new Vector3(PlayerManager.instance.transform.position.x,PlayerManager.instance.transform.position.y,-10f);
            //transform.position.z = new float (-10f);
            mousePosition = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void LateUpdate()
    {
        if (waitForSeconds > 0)
        {
            waitForSeconds -= Time.deltaTime;
        } else {
            SetOneLimit();
            FollowPlayer();
        }
    }

    void FindLimits(){
        boundaries = GameObject.FindGameObjectsWithTag("Boundary");
        allBounds = new Bounds[boundaries.Length];
        for (int i = 0; i < allBounds.Length; i++)
        {
            allBounds[i] = boundaries[i].gameObject.GetComponent<BoxCollider2D>().bounds;
        }
    }
    void SetOneLimit(){
        for (int i = 0; i < allBounds.Length; i++)
        {
            if (PlayerManager.instance.transform.position.x > allBounds[i].min.x && PlayerManager.instance.transform.position.x < allBounds[i].max.x && 
                PlayerManager.instance.transform.position.y > allBounds[i].min.y && PlayerManager.instance.transform.position.y < allBounds[i].max.y)
            {
                targetBounds = allBounds[i];
                return;
            }
        }
    }
    void FollowPlayer(){
        float xTarget = camBox.size.x < targetBounds.size.x ? Mathf.Clamp(PlayerManager.instance.transform.position.x, targetBounds.min.x + camBox.size.x/2, targetBounds.max.x - camBox.size.x/2) : 
            (targetBounds.min.x + targetBounds.max.x)/2;
        float yTarget = camBox.size.y < targetBounds.size.y ? Mathf.Clamp(PlayerManager.instance.transform.position.y, targetBounds.min.y + camBox.size.y/2, targetBounds.max.y - camBox.size.y/2) : 
            (targetBounds.min.y + targetBounds.max.y)/2;
        Vector3 target = new Vector3(xTarget, yTarget, -10f);
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
    public Vector3 GetMousePosition(){
        return mousePosition;
    }
    public bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
}

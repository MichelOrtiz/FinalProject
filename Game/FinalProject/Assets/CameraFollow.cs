using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private BoxCollider2D camBox;
    private ZoomCamera[] boundaries;
    private ZoomCamera[] allBounds;
    private ZoomCamera targetBounds;
    private float waitForSeconds = 0.5f;
    private Vector3 mousePosition;
    public float speed;
    public static CameraFollow instance = null;
    public bool camera1;
    public Camera camera;
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
        camera = GetComponent<Camera>();
        player = PlayerManager.instance.transform;
        camBox = GetComponent<BoxCollider2D>();
        FindLimits();
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
        //allBounds.Bounds
        boundaries = FindObjectsOfType<ZoomCamera>();
        //allBounds = boundaries;
        /*allBounds = new Bounds[boundaries.Length];
        for (int i = 0; i < allBounds.Length; i++)
        {
            allBounds[i] = boundaries[i].gameObject.GetComponent<BoxCollider2D>().bounds;
        }*/
    }
    void SetOneLimit(){
        for (int i = 0; i < boundaries.Length; i++)
        {
            if (player.position.x > boundaries[i].Bounds.min.x && player.position.x < boundaries[i].Bounds.max.x && player.position.y > boundaries[i].Bounds.min.y && player.position.y < boundaries[i].Bounds.max.y)
            {
                targetBounds = boundaries[i];
                return;
            }
        }
    }
    void FollowPlayer(){
        float xTarget = camBox.size.x < targetBounds.Bounds.size.x ? Mathf.Clamp(player.position.x, targetBounds.Bounds.min.x + camBox.size.x/2, targetBounds.Bounds.max.x - camBox.size.x/2) : 
            (targetBounds.Bounds.min.x + targetBounds.Bounds.max.x)/2;
        float yTarget = camBox.size.y < targetBounds.Bounds.size.y ? Mathf.Clamp(player.position.y, targetBounds.Bounds.min.y + camBox.size.y/2, targetBounds.Bounds.max.y - camBox.size.y/2) : 
            (targetBounds.Bounds.min.y + targetBounds.Bounds.max.y)/2;
        Vector3 target = new Vector3(xTarget, yTarget, -10f);
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        transform.position =  target;
        camera.orthographicSize = targetBounds.zCam;
    }
    public Vector3 GetMousePosition(){
        return mousePosition;
    }
    public bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
}

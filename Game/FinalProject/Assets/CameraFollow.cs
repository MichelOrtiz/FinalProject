using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    private PlayerManager player;
    private BoxCollider2D camBox;
    private ZoomCamera[] boundaries;
    private ZoomCamera[] allBounds;


    private ZoomCamera lastTargetBounds;
    private ZoomCamera targetBounds;

    private float waitForSeconds = 0.5f;
    private Vector3 mousePosition;
    
    public float speed;
    public float zoomSpeed;

    public static CameraFollow instance = null;
    public bool camera1;
    public Camera camera;


    [SerializeField] private bool canFollow = true;

    [SerializeField] private float zCamOffsset;


    private float xTarget, yTarget;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    public bool isCinematic;
    public Vector3 cinematicTarget;
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
                instance.gameObject.transform.position = PlayerManager.instance.GetPosition();
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }

    }
    void Start()
    {
        camera = GetComponent<Camera>();
        player = PlayerManager.instance;
        camBox = GetComponent<BoxCollider2D>();
        isCinematic = false;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        FindLimits();
        foreach (var bound in boundaries)
        {
            bound.EnterBounds += bound_Enter;
        }
    }

    void OnGUI()
    {
        if (camera1)
        {
            //transform.position = new Vector3(PlayerManager.instance.transform.position.x,PlayerManager.instance.transform.position.y,-10f);
            //transform.position.z = new float (-10f);
            Event   currentEvent = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = camera.pixelHeight - currentEvent.mousePosition.y;
            mousePosition = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camera.nearClipPlane));   
        }
    }

    void FixedUpdate()
    {
        if (waitForSeconds > 0)
        {
            waitForSeconds -= Time.deltaTime;
        } else {
            SetOneLimit();

            FindTarget();
            //if (canFollow)
            {
                FollowTarget();
            }

            //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
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
            if (player.GetPosition().x > boundaries[i].Bounds.min.x && player.GetPosition().x < boundaries[i].Bounds.max.x && player.GetPosition().y > boundaries[i].Bounds.min.y && player.GetPosition().y < boundaries[i].Bounds.max.y)
            {
                if (targetBounds != boundaries[i])
                {
                    targetBounds = boundaries[i];
                }
                return;
            }
        }
    }
    void FindTarget(){
        if(isCinematic){
            xTarget = cinematicTarget.x;
            yTarget = cinematicTarget.y;
        }else{
            xTarget = camBox.size.x < targetBounds.Bounds.size.x ? Mathf.Clamp(player.GetPosition().x, targetBounds.Bounds.min.x + camBox.size.x/2, targetBounds.Bounds.max.x - camBox.size.x/2) : 
            (targetBounds.Bounds.min.x + targetBounds.Bounds.max.x)/2;
            yTarget = camBox.size.y < targetBounds.Bounds.size.y ? Mathf.Clamp(player.GetPosition().y, targetBounds.Bounds.min.y + camBox.size.y/2, targetBounds.Bounds.max.y - camBox.size.y/2) : 
            (targetBounds.Bounds.min.y + targetBounds.Bounds.max.y)/2;
        }
        
        target = new Vector3(xTarget, yTarget, -10F);
        //transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        //transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, Time.deltaTime * speed);
        //transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //transform.position =  target;

    }

    void FollowTarget()
    {
        float xPos = Mathf.Lerp(transform.position.x, target.x, Time.fixedDeltaTime * speed);
        float yPos = Mathf.Lerp(transform.position.y, target.y, Time.fixedDeltaTime * speed);
        transform.position = new Vector3(xPos, yPos, -10f);
        //transform.position = Mathf.Lerp(transform.position, target, Time.fixedDeltaTime * speed);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetBounds.zCam, Time.fixedDeltaTime * zoomSpeed);
        //camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetBounds.zCam, Time.fixedDeltaTime * speed);
    }


    void bound_Enter(ZoomCamera sender)
    {
        //if (targetBounds != sender)
        {
            //targetBounds = sender;
            //if (targetBounds != null) camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetBounds.zCam * 2, Time.fixedDeltaTime * zoomSpeed / 2);
            
            //canFollow = false;

            //transform.position = Vector3.MoveTowards(transform.position, target, 25f * Time.deltaTime);
            //Invoke("FollowTarget", 1f);

            /*targetBounds.zCam *= 2;
            Invoke("ResetTargetZoom", 1);*/
        }
    }



    public Vector3 GetMousePosition(){
        return mousePosition;
    }

    
    public bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        FindLimits();
        Debug.Log(scene.name);
        Debug.Log(mode);
    }
}

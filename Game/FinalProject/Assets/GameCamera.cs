using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera instance = null;
    private Vector3 mousePosition;
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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        if (camera1)
        {
            //transform.position = new Vector3(PlayerManager.instance.transform.position.x,PlayerManager.instance.transform.position.y,-10f);
            mousePosition = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }
    }
    public Vector3 GetMousePosition(){
        return mousePosition;
    }
    public bool HasMouseMoved()
    {
        //I feel dirty even doing this 
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
}

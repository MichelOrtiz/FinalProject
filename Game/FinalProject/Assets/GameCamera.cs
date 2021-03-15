using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public static GameCamera instance = null;
    [SerializeField]
    private Vector3 mousePosition;
    void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(PlayerManager.instance.transform.position.x,PlayerManager.instance.transform.position.y,-10f);
    }

    // Update is called once per frame
    void Update()
    { 
        transform.position = new Vector3(PlayerManager.instance.transform.position.x,PlayerManager.instance.transform.position.y,-10f);
        mousePosition = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }
    public Vector3 GetMousePosition(){
        return mousePosition;
    }
}

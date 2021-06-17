using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public int index { get; set; }
    public bool isConnected { get; set; }
    public bool isBtnPressed {get;set;}
    RectTransform startPosition;
    Vector3 currentMousePosition;
    public GameObject btnConnect;
    public GameObject lightStatus;
    LineRenderer lr;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        startPosition = btnConnect.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBtnPressed){
            currentMousePosition = Input.mousePosition;
            StartCoroutine(AnimateLine(startPosition.anchoredPosition3D,currentMousePosition));
            if(Input.GetMouseButtonDown(1)){
                isBtnPressed = false;
                StopCoroutine(AnimateLine(startPosition.anchoredPosition3D,currentMousePosition));
            }
        }
    }
    IEnumerator AnimateLine(Vector3 a, Vector3 b){
        
        lr.SetPosition(0,a);
        lr.SetPosition(1,b);
    
        yield return null;
    }
    public void OnPressConect(){
        isBtnPressed = true;
    }
}

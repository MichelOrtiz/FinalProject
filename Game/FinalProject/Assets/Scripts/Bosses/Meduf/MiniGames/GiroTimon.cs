using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroTimon : MasterMinigame
{
    public int girar = 0;
    private float SceneHeight;
    private Vector3 PressPoint;
    private Quaternion StartRotation;

    [SerializeField] private float radius;
    
    [SerializeField] private float rotationAngleToWin;
    
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        SceneHeight = Screen.height;
        circleCollider = GetComponent<CircleCollider2D>();
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(cursorPos, transform.localPosition) <= radius)
        {
            PressPoint = Input.mousePosition;
            StartRotation = transform.rotation;
        }


        /*Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        transform.rotation = rotation;*/
        else if (Input.GetMouseButton(0))
        {
            float CurrentDistanceBetweenMousePosition = (Input.mousePosition - PressPoint).y;
            if (CurrentDistanceBetweenMousePosition < 0)
            {
                transform.rotation = StartRotation * Quaternion.Euler(Vector3.forward * (CurrentDistanceBetweenMousePosition/SceneHeight)*360);
            }


        }
        

        if (transform.localEulerAngles .z> 0 && transform.localEulerAngles.z <= rotationAngleToWin)
        {
            //Debug.Log("minigame won!");
            OnWinMinigame();
        }
    }
}

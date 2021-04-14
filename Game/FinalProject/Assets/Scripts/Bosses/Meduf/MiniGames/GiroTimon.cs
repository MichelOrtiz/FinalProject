using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroTimon : MonoBehaviour
{
    public int girar = 0;
    private float SceneHeight;
    private Vector3 PressPoint;
    private Quaternion StartRotation;
    // Start is called before the first frame update
    void Start()
    {
        SceneHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (girar == 1)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            transform.rotation = rotation;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            PressPoint = Input.mousePosition;
            StartRotation = transform.rotation;
        }
        else if (Input.GetMouseButton(0))
        {
            float CurrentDistanceBetweenMousePosition = (Input.mousePosition - PressPoint).y;
            transform.rotation = StartRotation * Quaternion.Euler(Vector3.forward * (CurrentDistanceBetweenMousePosition/SceneHeight)*360);
        }
    }
    void OnMouseDown(){
        //girar = 1;
    }
    void OnMouseUp(){
        girar = 0;
    }
}

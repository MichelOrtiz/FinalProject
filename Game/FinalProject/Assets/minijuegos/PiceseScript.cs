using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PiceseScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition = false;
    public bool Selected;
        void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(-256f,-250f),Random.Range(6f,13f));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,RightPosition) < .5f)
        {
            if (!Selected)
            {
                if(InRightPosition == false){
                    transform.position=RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
            
        }
    }
}

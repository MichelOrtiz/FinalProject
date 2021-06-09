using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PiceseScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition = false;
    public bool Selected;
    int lado = 0;
        void Start()
    {
        RightPosition = transform.position;
        if (lado == 0)
        {
            transform.position = new Vector3(Random.Range(220,180),Random.Range(310,120));
            lado++;
        }else{
            transform.position = new Vector3(Random.Range(590,550),Random.Range(310,120));
            lado--;
        }
            
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

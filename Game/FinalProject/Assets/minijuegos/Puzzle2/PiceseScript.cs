using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PiceseScript : MasterMinigame
{
    [SerializeField] private Vector2 RightPosition;
    public bool InRightPosition = false;
    public bool Selected;
    int lado = 0;
    public bool correcta = false;
    [SerializeField] float rango;

    void Start()
    {
        RightPosition = transform.position;
        lado = Random.Range(0,2);
        if (lado == 0)
        {
            transform.localPosition = new Vector2(Random.Range(-250, -250),Random.Range(-150, 150));
        }else{
            transform.localPosition = new Vector2(Random.Range(250, 250),Random.Range(-150, 50));
        }
    }

    void Update()
    {        

        if (Vector2.Distance(transform.position,RightPosition) < rango)
        {
            if (!Selected)
            {
                if(InRightPosition == false){
                    transform.position=RightPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    correcta = true;
                }
            }
        }
    }
}

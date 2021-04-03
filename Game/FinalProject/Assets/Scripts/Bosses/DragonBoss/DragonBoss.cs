using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : MonoBehaviour
{
    [SerializeField]private GameObject projectile;
    [SerializeField]private Transform head;
    [SerializeField]private Transform tail;
    [SerializeField]private float shootInterval;
    private float currentTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=shootInterval){
            //ShootFireBall
        }
    }
    void ShootFireBall(){
        Instantiate(projectile,head.position,Quaternion.identity);
    }
}

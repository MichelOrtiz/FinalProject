using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    private float lengthx, lengthy, startposx, startposy;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        lengthx = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthy = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        float tempx = (cam.transform.position.x * (1-parallaxEffect));
        float distx = (cam.transform.position.x * parallaxEffect);
        float tempy = (cam.transform.position.y * (1-parallaxEffect));
        float disty = (cam.transform.position.y * parallaxEffect);
        cam.transform.position = new Vector3(startposx + distx, startposy + disty, transform.position.z);
        if (tempx > startposx + lengthx)
        {
            startposx += lengthx;
        }else if (tempx < startposx - lengthx)
        {
            startposx -= lengthx;
        }
        if (tempy > startposy + lengthy)
        {
            startposy += lengthy;
        }else if (tempy < startposy - lengthy)
        {
            startposy -= lengthy;
        }
    }
}

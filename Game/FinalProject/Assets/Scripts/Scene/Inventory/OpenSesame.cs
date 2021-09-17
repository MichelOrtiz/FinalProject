using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSesame : MonoBehaviour
{
    public float rango = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
        if(distance <= rango){
            if(Input.GetKeyDown(KeyCode.E)){
                CofreUI.instance.gameObject.SetActive(true);
            }
        }
    }
}

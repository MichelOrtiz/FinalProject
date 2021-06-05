using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessMinigame : MonoBehaviour
{
    public float radius;
    public GameObject minigameObject;
    private Minigame minigame;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Place where the oject is - place where Nico is
        float distance = Vector2.Distance(PlayerManager.instance.transform.position, transform.position);
        if(Input.GetKeyDown(KeyCode.E)&&distance<radius){
            //spawns the minigame as a Unity object so that it recognizes its methods, then runs its code.
            minigame = Instantiate(minigameObject).GetComponent<Minigame>();  
            minigame.StartMinigame();
        }
    }
}

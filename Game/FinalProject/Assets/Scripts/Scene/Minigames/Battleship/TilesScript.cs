using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesScript : MonoBehaviour
{
    BattleshipManager battleshipManager;
    Ray ray;
    RaycastHit hit;

    private bool missileHit = false;
    Color32[] hitColor = new Color32[2];
    // Start is called before the first frame update
    void Start()
    {
        battleshipManager = GameObject.Find("BattleshipManager").GetComponent<BattleshipManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("0");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)){
            Debug.Log("1");
            if(Input.GetMouseButtonDown(0) && hit.collider.gameObject.name == gameObject.name){
                Debug.Log("2");
                if(missileHit == false){
                    Debug.Log("3");
                    battleshipManager.TileClicked(hit.collider.gameObject);
                }
            }
        }
    }
}

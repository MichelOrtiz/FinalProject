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
    }

    public void onClickTile(){
        if(missileHit == false){
            battleshipManager.TileClicked(gameObject);
        }
        
    }
    
}

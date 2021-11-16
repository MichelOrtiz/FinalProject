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
    public byte numberId;

    // Start is called before the first frame update
    void Start()
    {
        battleshipManager = GameObject.Find("BattleshipManager").GetComponent<BattleshipManager>();
        //hitColor[0] = gameObject.GetComponent<MeshRenderer>().material.color;
        //hitColor[1] = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickTile(){
        if(missileHit == false){
            battleshipManager.TileClicked(this);
        }
        
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("BattleshipMissile")){
            missileHit = true;
        }
        //else if Where the enemy missile would go
    }

    public void SetTileColor(int index, Color32 color){
        hitColor[index] = color;
    } 
    public void SwitchColors(int colorIndex){
        GetComponent<Renderer>().material.color = hitColor[colorIndex];
    }   
}

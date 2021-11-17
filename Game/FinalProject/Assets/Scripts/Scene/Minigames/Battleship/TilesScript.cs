using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesScript : MonoBehaviour
{
    BattleshipManager battleshipManager;
    Ray ray;
    RaycastHit hit;

    private bool missileHit = false;
    public byte numberId;
    public bool tileClicked;

    [Header("FX")]
    [SerializeField] private Color32 hitColor;
    [SerializeField] private Color32 missedColor;

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

    public void onClickTile()
    {
        if(!tileClicked || GetComponent<Image>().color == battleshipManager.flagColor)
        {
            battleshipManager.TileClicked(this);
        }
        
    }


    public void SetTileColor(Color32 color){
        GetComponent<Image>().color = color;
    } 
}

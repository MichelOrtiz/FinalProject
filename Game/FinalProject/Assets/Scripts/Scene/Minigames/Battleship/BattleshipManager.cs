using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleshipManager : MonoBehaviour
{
    public GameObject[] ships;

    [Header("HUD")]
    public Button nextButton;
    public Button rotateButton;
    public Text topText;
    [Header("Objects")]
    public GameObject missilePrefab;


    private bool setupComplete = false;
    private bool playerTurn = true;
    private int shipIndex = 0;
    private ShipScript shipScript;
    public EnemyScript enemyScript;
    private List<int[]> enemyShips;

    private int enemyShipCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        shipScript = ships[shipIndex].GetComponent<ShipScript>();
        nextButton.onClick.AddListener(() => NextShipClicked());
        rotateButton.onClick.AddListener(() => RotateClicked());
        ////if (Input.GetMouseButtonUp(0)) {
        enemyShips = enemyScript.PlaceEnemyShips();
    }

    private void NextShipClicked(){
        //Condition to only do this if there are still ships
        if(shipIndex <= ships.Length - 2){
            shipIndex++;
            shipScript = ships[shipIndex].GetComponent<ShipScript>();
            //shipScript.FlashColor(Color.yellow);
        } else{
            enemyScript.PlaceEnemyShips();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TileClicked(GameObject tile){
        if(setupComplete && playerTurn){
            //Drop missile - BOOM
        } else if(!setupComplete){
            PlaceShip(tile);
            shipScript.SetClickedTile(tile);
        }
    } 

    private void PlaceShip(GameObject tile){
        shipScript = ships[shipIndex].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector2 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[shipIndex].transform.position = newVec;
    }

    private void RotateClicked(){
        shipScript.RotateShip();
    }

    void SetShipClickedTile(GameObject tile){
        shipScript.SetClickedTile(tile);
    }

    public void CheckHit(GameObject tile){
        //Take the tile's individual number and name, and compare them with the enemy ships coordinates
        int tileNum = Int32.Parse(Regex.Match(tile.name, @"\d+").Value);
        int hitCount = 0;
        foreach (int[] tileNumArray in enemyShips){
            if(tileNumArray.Contains(tileNum)){
                for(int i=0; i< tileNumArray.Length; i++){
                    if(tileNumArray[i]== tileNum){
                        /*if our tile index matches the enemy tile number, 
                        we hit the ship(truck indexes are -5)*/
                        tileNumArray[i] = -5;
                        hitCount++;
                    }
                    else if(tileNumArray[i]==-5){
                        //We have already hit the tile
                        hitCount++;
                    }
                }//check wether we have sunk the ship
                if(hitCount == tileNumArray.Length){
                    enemyShipCount--;
                    topText.text = "SUNK!!";
                }else{
                    topText.text = "HIT!!";
                }
                break;
            }
            if(hitCount == 0){
                topText.text = "Missed. There is no ship there";
            }
        }
    }
}

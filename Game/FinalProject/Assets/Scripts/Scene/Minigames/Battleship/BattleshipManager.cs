using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleshipManager : MonoBehaviour
{
    public GameObject[] ships;

    [Header("HUD")]
    public Button nextButton;
    public Button rotateButton;

    private bool setupComplete = false;
    private bool playerTurn = true;
    private int shipIndex = 0;
    private ShipScript shipScript;

    // Start is called before the first frame update
    void Start()
    {
        shipScript = ships[shipIndex].GetComponent<ShipScript>();
        nextButton.onClick.AddListener(() => NextShipClicked());
        rotateButton.onClick.AddListener(() => RotateClicked());
        ////if (Input.GetMouseButtonUp(0)) {
    }

    private void NextShipClicked(){
        //Condition to only do this if there are still ships
        if(shipIndex <= ships.Length - 2){
            shipIndex++;
            shipScript = ships[shipIndex].GetComponent<ShipScript>();
            //shipScript.FlashColor(Color.yellow);
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
}

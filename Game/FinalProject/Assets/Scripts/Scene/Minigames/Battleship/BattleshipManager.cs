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
    }

    private void NextShipClicked(){
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
        Debug.Log("Si se le da click al tile");
        if(setupComplete && playerTurn){
            //Drop missile - BOOM
        } else if(!setupComplete){
            Debug.Log("Si manda a llamar a Placeship");
            PlaceShip(tile);
        }
    } 

    private void PlaceShip(GameObject tile){
        Debug.Log("Entra a placeship");
        shipScript = ships[shipIndex].GetComponent<ShipScript>();
        shipScript.ClearTileList();
        Vector3 newVec = shipScript.GetOffsetVec(tile.transform.position);
        ships[shipIndex].transform.localPosition = newVec;
        Debug.Log("Cambia de posicion");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    List<GameObject> touchTiles = new List<GameObject>();
    public float xOffset = 0;
    public float yOffset = 0;
    private float nextYRotation = 90f;
    private GameObject clickedTile;
    //Maybe:
    int hitCount = 0;
    int shipSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearTileList(){
        touchTiles.Clear();
    }

    public Vector2 GetOffsetVec(Vector2 tilePos){
        return new Vector2(tilePos.x + xOffset, tilePos.y + yOffset);
    }

    public void RotateShip(){
        touchTiles.Clear();
        transform.eulerAngles += new Vector3(0, 0, nextYRotation);
        nextYRotation *= -1;
        float temp = xOffset;
        xOffset = yOffset;
        yOffset = temp;
        //SetPosition(clickedTile.transform.position);
    }

    public void SetPosition(Vector2 newVec){
        transform.localPosition = new Vector2(newVec.x + xOffset, newVec.y + yOffset);
    }

    public void SetClickedTile(GameObject tile){
        clickedTile = tile;
    }
    //check to use when we place ships. Probably won't need it...
    public bool OnGameBoard(){
        return touchTiles.Count == shipSize;
    }

    //Check if the ship has sunk??
    public bool HitCheckSank(){
        hitCount++;
        return shipSize <=hitCount;
    }

}

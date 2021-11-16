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
    //Maybe, maybe not needed:
    private Material[] allMaterial;
    List<Color> allColors = new List<Color>();
    int hitCount = 0;
    public int shipSize;

   /* private void Start(){
        allMaterial = GetComponent<Renderer>().materials;
        for(int i=0; )
    }*/

    /*Probably doesn't matter either. checks the tiles the ships in contact with??
    Really feeling like this script shouldn't exist :p
    */
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("battleshipTile")){
            touchTiles.Add(collision.gameObject);
        }
    }

    public void ClearTileList(){
        touchTiles.Clear();
    }

    public Vector2 GetOffsetVec(Vector2 tilePos){
        return new Vector2(tilePos.x + xOffset, tilePos.y + yOffset);
    }

    public void RotateShip(){
        if(clickedTile == null) return;
        touchTiles.Clear();
        transform.eulerAngles += new Vector3(0, 0, nextYRotation);
        nextYRotation *= -1;
        float temp = xOffset;
        xOffset = yOffset;
        yOffset = temp;
        SetPosition(clickedTile.transform.position);
    }

    public void SetPosition(Vector2 newVec){
        ClearTileList();
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

    //wtf is this? What does it do??????
    public void FlashColor(Color tempColor){
        foreach(Material mat in allMaterial){
            mat.color = tempColor;
        }
    }

    private void ResetColor(){
        int i=0;
        foreach(Material mat in allMaterial){
            mat.color = allColors[i++];
        }
    }

}

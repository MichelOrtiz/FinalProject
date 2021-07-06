using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    List<GameObject> touchTiles = new List<GameObject>();
    public float xOffset = 0;
    public float yOffset = 0;

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

    
}

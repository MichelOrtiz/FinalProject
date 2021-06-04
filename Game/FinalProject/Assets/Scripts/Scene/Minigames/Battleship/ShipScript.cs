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

    public Vector3 GetOffsetVec(Vector3 tilePos){
        return new Vector3(tilePos.x + xOffset, tilePos.y + yOffset, 0);
    }
}

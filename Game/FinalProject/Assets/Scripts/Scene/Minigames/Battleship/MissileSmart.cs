using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName="MissileSmart", menuName = "Battleship/MissileSmart")]
public class MissileSmart : MissileScript
{
    public override void Affect()
    {
        

        var manager = FindObjectOfType<BattleshipManager>();
        var tiles = ScenesManagers.GetObjectsOfType<TilesScript>();
        var ships = manager.enemyShips;

        byte nearestRange = 0;
        byte nearestHit = numberId;

        foreach (int[] tileNumArray in ships)
        {
            for (int i = 0; i < tileNumArray.Length; i++)
            {
                var range = Mathf.Abs(tileNumArray[i] - numberId);
                var tile = tiles.Find(tile => tile.numberId == tileNumArray[i]);

                if ( (nearestRange == 0 || range < nearestRange) && (tile != null && !tile.tileClicked))
                {
                    nearestHit = (byte)tileNumArray[i];
                }
            }
        }
        
        var tileClicked = tiles.Find(tile => tile.numberId == numberId);
        tileClicked.tileClicked = false;
        
        var hit = tiles.Find(tile => tile.numberId == nearestHit);
        hit.tileClicked = true;

        if (manager.CheckHit(nearestHit, true))
        {
            tiles.Find(tile => tile.numberId == nearestHit).SetTileColor(manager.hitColor);
        }

    }
}

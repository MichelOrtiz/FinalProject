using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName="MissileShip", menuName = "Battleship/MissileShip")]
public class MissileShip : MissileScript
{
    public override void Affect()
    {
        //base.Affect();
        var manager = FindObjectOfType<BattleshipManager>();
        var ships = manager.enemyShips;
        bool hit = false;

        var tiles = ScenesManagers.GetObjectsOfType<TilesScript>();
        foreach (int[] tileNumArray in ships)
        {
            if(tileNumArray.Contains(numberId))
            {
                hit = true;
                for(int i=0; i< tileNumArray.Length; i++)
                {
                    var tileHit = tiles.Find(tile => tile.numberId == tileNumArray[i]);
                    if (tileHit != null)
                    {
                        tileHit.tileClicked = true;
                        tileHit.SetTileColor(manager.hitColor);
                    }
                }
                manager.EnemyShipCount--; 
                break;
            }
        }
        // didn't hit
        if (!hit)
        {
            tiles.Find(tile => tile.numberId == numberId)?.SetTileColor(manager.missedColor);
        }
    }
}

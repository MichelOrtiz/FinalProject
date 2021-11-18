using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName="MissileShip", menuName = "Battleship/MissileShip")]
public class MissileShip : MissileScript
{
    public override void Affect()
    {
        base.Affect();
        var manager = FindObjectOfType<BattleshipManager>();
        var ships = manager.enemyShips;
        int hitCount = 0;

        foreach (int[] tileNumArray in ships)
        {
            if(tileNumArray.Contains(numberId))
            {

                for(int i=0; i< tileNumArray.Length; i++)
                {
                    var tiles = ScenesManagers.GetObjectsOfType<TilesScript>();
                    var tileHit = tiles.Find(tile => tile.numberId == tileNumArray[i]);
                    if (tileHit != null)
                    {
                        tileHit.tileClicked = true;
                        tileHit.SetTileColor(manager.hitColor);
                    }
                    hitCount++;
                }//check whether we have sunk the ship
                manager.EnemyShipCount--; 
                break;
            }
            if(hitCount == 0){
                //topText.text = "Missed. There is no ship there";
            }
        }
    }
}
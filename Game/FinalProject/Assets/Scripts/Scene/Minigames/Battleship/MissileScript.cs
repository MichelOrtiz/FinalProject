using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Missile", menuName = "Battleship/Missile")]
public class MissileScript : ScriptableObject
{
    public byte numberId {get;set;}
    public enum Type
    {
        Normal, 
        Ship, 
        Radar, 
        Holy,
        Smart
    }
    public Type type = Type.Normal;

    public virtual void Affect()
    {
        var manager = FindObjectOfType<BattleshipManager>();
        var tiles = ScenesManagers.GetObjectsOfType<TilesScript>();
        var tile = tiles.Find(tile => tile.numberId == numberId);
        tile.tileClicked = true;
        if (tile != null)
        {
            if (manager.CheckHit(numberId, true))
            {
                tile?.SetTileColor(manager.hitColor);
            }
            else
            {
                tile?.SetTileColor(manager.missedColor);
            }
        }
    }
}

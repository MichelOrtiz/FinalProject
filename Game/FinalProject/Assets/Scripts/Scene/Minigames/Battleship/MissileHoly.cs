using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName="MissileHoly", menuName = "Battleship/MissileHoly")]
public class MissileHoly : MissileScript
{
    [SerializeField] private byte radius;
    public override void Affect()
    {
        base.Affect();

        byte[] verticalAdy = new byte[radius * 2];
        byte[] horizontalAdy = new byte[radius * 2];
        byte[] normDiagonal = new byte[radius * 2];
        byte[] invDiagonal = new byte[radius * 2];

        verticalAdy[0] = (byte)(numberId + 10);
        horizontalAdy[0] = (byte)(numberId + 1);
        normDiagonal[0] = (byte)(numberId + 11);
        invDiagonal[0] = (byte)(numberId + 9);

        verticalAdy[radius] = (byte)(numberId - 10);
        horizontalAdy[radius] = (byte)(numberId - 1);
        normDiagonal[radius] = (byte)(numberId - 11);
        invDiagonal[radius] = (byte)(numberId - 9);

        for (int i = 1; i < radius; i++)
        {
            verticalAdy[i] = (byte)(verticalAdy[i-1] + 10);
            horizontalAdy[i] = (byte)(horizontalAdy[i-1] + 1);
            normDiagonal[i] = (byte)(normDiagonal[i-1] + 11);
            invDiagonal[i] = (byte)(invDiagonal[i-1] + 9);
        }


        for (int i = radius + 1 ; i < radius * 2; i++)
        {
            verticalAdy[i] = (byte)(verticalAdy[i-1] - 10);
            horizontalAdy[i] = (byte)(horizontalAdy[i-1] - 1);
            normDiagonal[i] = (byte)(normDiagonal[i-1] - 11);
            invDiagonal[i] = (byte)(invDiagonal[i-1] - 9);
        }


        SetColors(verticalAdy);
        SetColors(horizontalAdy);
        SetColors(normDiagonal);
        SetColors(invDiagonal);

    }

    void SetColors(byte[] array)
    {
        var manager = FindObjectOfType<BattleshipManager>();
        var tiles = ScenesManagers.GetObjectsOfType<TilesScript>();

        for (int i = 0; i < radius * 2; i++)
        {
            var tileHit = tiles.Find(tile => tile.numberId == array[i]);
            if (tileHit != null && !tileHit.tileClicked)
            {
                tileHit.tileClicked = true;
                if (manager.CheckHit(array[i], true))
                {
                    tileHit.SetTileColor(manager.hitColor);
                }
                else
                {
                    tileHit.SetTileColor(manager.missedColor);
                }
            }
        }

    }
}

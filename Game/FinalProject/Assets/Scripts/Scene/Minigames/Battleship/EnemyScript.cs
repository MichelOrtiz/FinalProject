using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public List<int[]> PlaceEnemyShips(){
        //Lists for the location of the enemy ships
        List<int[]> enemyShips = new List<int[]>{
            new int[]{-1, -1, -1, -1, -1},
            new int[]{-1, -1, -1, -1},
            new int[]{-1, -1, -1},
            new int[]{-1, -1, -1},
            new int[]{-1, -1}
        };

        int[] gridNumbers = Enumerable.Range(1, 100).ToArray();
        bool taken = true;
        //Goes through all the tiles
        foreach(int[] tileNumArray in enemyShips){
            taken = true;
            while(taken == true){
                taken = false;
                //index for where enemy ships will be
                int shipNose = UnityEngine.Random.Range(0, 99);
                /*rotation is randomized and we use it to figure uout 
                wether we punt a 10 or a 1 in the rotation value*/
                int rotateBool = UnityEngine.Random.Range(0, 2);
                int minusAmount = rotateBool == 0 ? 10 : 1;
                for(int i=0; i<tileNumArray.Length; i++){
                    /*checks that the ship isn't out of the grid or on a tile 
                    already taken*/
                    if((shipNose - (minusAmount * i)) < 0 || gridNumbers[shipNose - i * minusAmount]<0){
                        taken = true;
                        break;
                    }
                    /*if the ship is horizontal, makes sure it's on the same row*/
                    else if(minusAmount == 1 && shipNose /10 != ((shipNose - i * minusAmount)-1) /10){
                        taken = true;
                        break;
                    }
                }
                /*if the tile isn't taken, loop through the tile Numbers and 
                assign them to a ship array*/
                if(taken == false){
                    for(int j = 0; j <tileNumArray.Length; j++){
                        tileNumArray[j] = gridNumbers[shipNose - j * minusAmount];
                        gridNumbers[shipNose - j * minusAmount] = -1;
                    }
                }
            }
        }
        foreach(int[] numArray in enemyShips){
            string temp = " ";
            for(int i = 0; i< numArray.Length; i ++){
                temp += ", " + numArray[i];
            }
            Debug.Log(temp);
        }
        return enemyShips;
    }
}

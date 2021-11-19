using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleshipManager : MasterMinigame
{
    [Header("HUD")]
    [SerializeField] private GameObject endPopUp;
    public Text topText;

    [SerializeField] private Text missilesText;
    [SerializeField] private TMPro.TextMeshProUGUI currentMissileText;
    [SerializeField] private TMPro.TextMeshProUGUI currentMissileDescription;
    
    [SerializeField] private Text enemyShipsText;

    [Header("Objects")]
    public Color32 hitColor;
    public Color32 missedColor;
    public Color32 flagColor;


    public EnemyScript enemyScript;
    public List<int[]> enemyShips;

    private int enemyShipCount = 5;
    public int EnemyShipCount {
        get => enemyShipCount; 
        set
        {
            enemyShipCount = value;
            enemyShipsText.text = enemyShipCount.ToString();
            if (enemyShipCount == 0)
            {
                OnWinMinigame();
            }
        } 
    
    }



    [Header("Missiles")]
    [SerializeField] private List<MissileScript> missiles;
    [SerializeField] private byte playerMissiles;
    public byte PlayerMissiles { get=>playerMissiles; 
        set
        {

            playerMissiles = value;
            missilesText.text = value.ToString();

        }
    }
    [SerializeField] private MissileScript currentMissile;
    byte index;
        
    void Awake()
    {
        playerMissiles = (byte)missiles.Count;
        missilesText.text = PlayerMissiles.ToString();
    }

    void Start()
    {
        enemyShips = enemyScript.PlaceEnemyShips();
        currentMissile = missiles[index];
        enemyShipsText.text = enemyShipCount.ToString();
        UpdateMissileInfo();
    }


    public void TileClicked(TilesScript tile)
    {
        if (index < missiles.Count)
        {
            Vector2 tilePos = tile.transform.position;
            //tilePos.y += 
            var missile = Instantiate(currentMissile);
            missile.numberId = tile.numberId;
            missile.Affect();

            index++;

            if (index >= missiles.Count)
            {
                if (EnemyShipCount != 0)
                {
                    OnLoseMinigame();
                    return;
                }
            }
            currentMissile = missiles[index];
            UpdateMissileInfo();
            PlayerMissiles--;
        }
    } 

    
    public bool CheckHit(byte tileNum, bool sinkEnabled)
    {
        //Take the tile's individual number and name, and compare them with the enemy ships coordinates
        //int tileNum = Int32.Parse(Regex.Match(tile.name, @"\d+").Value);
        int hitCount = 0;
        foreach (int[] tileNumArray in enemyShips){
            if(tileNumArray.Contains(tileNum)){
                for(int i=0; i< tileNumArray.Length; i++){
                    if(tileNumArray[i]== tileNum){
                        /*if our tile index matches the enemy tile number, 
                        we hit the ship(truck indexes are -5)*/
                        if (sinkEnabled)
                        {
                            tileNumArray[i] = -5;
                        }
                        hitCount++;
                    }
                    else if(tileNumArray[i]==-5){
                        //We have already hit the tile
                        hitCount++;
                    }
                }//check whether we have sunk the ship


                if (sinkEnabled)
                {
                    if(hitCount == tileNumArray.Length){
                        EnemyShipCount--;
                        topText.text = "SUNK!!";
                    }else{
                        topText.text = "HIT!!";
                    }
                }
                return true;
                break;
            }
            if(hitCount == 0){
                topText.text = "Missed. There is no ship there";
            }
        }
        return false;
    }

    void UpdateMissileInfo()
    {
        currentMissileText.text = currentMissile.missileName + ":";
        currentMissileDescription.text = currentMissile.description;
    }
}

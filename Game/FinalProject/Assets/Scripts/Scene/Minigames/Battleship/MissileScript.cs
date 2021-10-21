using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    // Start is called before the first frame update
    private BattleshipManager battleshipManager;
    void Start()
    {
        battleshipManager = GameObject.Find("BattleshipManager").GetComponent<BattleshipManager>();
    }

    private void OnCollisionEnter(Collision collision){
        battleshipManager.CheckHit(collision.gameObject);
        Destroy(gameObject);
    }
}

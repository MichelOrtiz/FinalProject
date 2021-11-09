using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalera : MonoBehaviour
{
    public bool isLadder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Platform"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ground") || collision.CompareTag("Platform"))
        {
            isLadder = false;
        }
    }
}

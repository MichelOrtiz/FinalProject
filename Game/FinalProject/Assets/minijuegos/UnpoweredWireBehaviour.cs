using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpoweredWireBehaviour : MonoBehaviour
{
    public bool prueba = false;
    public UnpoweredWireStat unpoweredWireS;
    void Start()
    {
        unpoweredWireS = gameObject.GetComponent<UnpoweredWireStat>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageLight();
    }
    void OnTriggerEnter2d(Collider2D collision){
        if (collision.GetComponent<PoweredWireStats>())
        {
            prueba = true;
            PoweredWireStats poweredWireS = collision.GetComponent<PoweredWireStats>();
            if (poweredWireS.objectColor == unpoweredWireS.objectColor)
            {
                poweredWireS.connected = true;
                unpoweredWireS.connected = true;
                poweredWireS.connectedPosition = gameObject.transform.position;
            }
        }
    }
    void OnTriggerExit2d(Collider2D collision){
        if (collision.GetComponent<PoweredWireStats>())
        {
            PoweredWireStats poweredWireS = collision.GetComponent<PoweredWireStats>();
            poweredWireS.connected = false;
            unpoweredWireS.connected = false;
        }
    }
    void ManageLight(){
        if (unpoweredWireS.connected)
        {
            unpoweredWireS.PoweredLightOn.SetActive(true);
            unpoweredWireS.PoweredLightOff.SetActive(false);
        }else
        {
            unpoweredWireS.PoweredLightOff.SetActive(true);
            unpoweredWireS.PoweredLightOn.SetActive(false);
        }
    }
}

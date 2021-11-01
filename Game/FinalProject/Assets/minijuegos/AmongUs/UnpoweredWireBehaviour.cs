using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpoweredWireBehaviour : MonoBehaviour
{
    public UnpoweredWireStat unpoweredWireS;
    [SerializeField] private PoweredWireBehaviour poweredWireAttached;
    [SerializeField] private float checkRadius;
    private PoweredWireStats poweredWireS;
    void Start()
    {
        unpoweredWireS = gameObject.GetComponent<UnpoweredWireStat>();

        poweredWireS = poweredWireAttached.GetComponent<PoweredWireStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, poweredWireAttached.transform.position);
        if (distance <= checkRadius)
        {
            //if (poweredWireS.objectColor == unpoweredWireS.objectColor)
            {
                poweredWireS.connected = true;
                unpoweredWireS.connected = true;
                poweredWireS.connectedPosition = gameObject.transform.position;
            }
        }
        ManageLight();
    }
    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(gameObject + " Collision: " + collision);
        if (collision.GetComponent<PoweredWireStats>())
        {
            PoweredWireStats poweredWireS = collision.GetComponent<PoweredWireStats>();
            if (poweredWireS.objectColor == unpoweredWireS.objectColor)
            {
                poweredWireS.connected = true;
                unpoweredWireS.connected = true;
                poweredWireS.connectedPosition = gameObject.transform.position;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision){
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

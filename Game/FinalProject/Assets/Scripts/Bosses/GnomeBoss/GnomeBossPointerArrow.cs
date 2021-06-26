using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeBossPointerArrow : MonoBehaviour
{
    [SerializeReference] private Switch referenceSwitch; 
    
    private PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector2(0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (referenceSwitch == null)
        {
            referenceSwitch = FindObjectOfType<Switch>();
        }
        else
        {
            float angle = MathUtils.GetAngleBetween(transform.position, referenceSwitch.transform.position);
            Debug.Log("angle to switch: " + angle);
            transform.eulerAngles = new Vector3(0,0,angle);
            //transform.eulerAngles = transform.TransformVector(MathUtils.GetVectorFromAngle(angle));
        }
    }
}

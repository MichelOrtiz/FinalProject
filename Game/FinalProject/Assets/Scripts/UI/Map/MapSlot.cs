using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MapSlot : MonoBehaviour
{
    int Scene;
    bool isObtained;
    bool isHere;
    [SerializeField] Button boton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI(){
        if (isHere)
        {
            GetComponent<Image>().color = Color.red;
        }else
        {
            GetComponent<Image>().color = Color.white;
        }
        if (isObtained)
        {
            boton.enabled = true;
            boton.gameObject.GetComponent<Image>().color = Color.white;
        }else
        {
            boton.enabled = false;
            boton.gameObject.GetComponent<Image>().color = Color.gray;
        }
    }
}

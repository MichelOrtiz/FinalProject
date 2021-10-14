using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MapSlot : MonoBehaviour
{
    public bool isObtained;
    public bool isHere;
    public int Scene;
    [SerializeField] GameObject zonaMapa;
    [SerializeField] Button boton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateUI(){
        if (isHere)
        {

        }else
        {

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
    public void OnBottonClicked(){
        zonaMapa.SetActive(true);
    }
}

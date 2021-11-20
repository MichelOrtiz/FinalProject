using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MapSlot : MonoBehaviour
{
    public bool isObtained;
    public int Scene;
    public bool isHere {get => SceneController.instance.currentScene == Scene;}
    [SerializeField] GameObject fondo;
    [SerializeField] Button boton;
    [SerializeField] GameObject nicoIsHere;
    [SerializeField] Sprite zonaMapa;
    [SerializeField] Image imagenZona;

    public void UpdateUI(){
        if (isHere)
        {
            nicoIsHere.SetActive(true);
        }else
        {
            nicoIsHere.SetActive(false);
        }
        if (isObtained)
        {
            boton.enabled = true;
            boton.gameObject.GetComponent<Image>().color = Color.white;
        }else
        {
            boton.enabled = false;
            boton.gameObject.GetComponent<Image>().color = Color.clear;
        }
    }
    public void OnBottonClicked(){
        fondo.SetActive(true);
        imagenZona.sprite = zonaMapa;
    }
}

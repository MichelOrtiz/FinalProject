using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonesMedufIncMinigame5 : MonoBehaviour
{
    public GameObject turnOffBlue, turnOffGreen, turnOffRed, turnOffYellow, turnOffPurple, expBlue, expGreen, expRed, expYellow, expPurple, expBlues, expGreens, expReds, expYellows, expPurples;
    public Toggle Purple, Yellow, Red, Green, Blue;
    public bool blue, green, red, yellow, purple;
    void Start()
    {
        blue = false;
        green = false;
        red = false;
        yellow = false;
        purple = false;
        expBlues.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
        expGreens.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
        expYellows.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
        expReds.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
        expPurples.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
        expBlues.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
        expGreens.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
        expYellows.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
        expReds.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
        expPurples.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (blue)
        {
            expBlue.gameObject.GetComponent<PoweredWireBehaviour>().enabled = true;
        }else
        {
            expBlue.gameObject.GetComponent<PoweredWireBehaviour>().enabled = false;
        }
        if (green)
        {
            expGreen.gameObject.GetComponent<PoweredWireBehaviour>().enabled = true;
        }else
        {
            expGreen.gameObject.GetComponent<PoweredWireBehaviour>().enabled = false;
        }
        if (yellow)
        {
            expYellow.gameObject.GetComponent<PoweredWireBehaviour>().enabled = true;
        }else
        {
            expYellow.gameObject.GetComponent<PoweredWireBehaviour>().enabled = false;
        }
        if (red)
        {
            expRed.gameObject.GetComponent<PoweredWireBehaviour>().enabled = true;
        }else
        {
            expRed.gameObject.GetComponent<PoweredWireBehaviour>().enabled = false;
        }
        if (purple)
        {
            expPurple.gameObject.GetComponent<PoweredWireBehaviour>().enabled = true;
        }else
        {
            expPurple.gameObject.GetComponent<PoweredWireBehaviour>().enabled = false;
        }
        if (blue&&red&&yellow&&green&&purple){
            expBlues.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = true;
            expGreens.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = true;
            expYellows.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = true;
            expReds.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = true;
            expPurples.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = true;
            expBlues.gameObject.GetComponent<UnpoweredWireStat>().enabled = true;
            expGreens.gameObject.GetComponent<UnpoweredWireStat>().enabled = true;
            expYellows.gameObject.GetComponent<UnpoweredWireStat>().enabled = true;
            expReds.gameObject.GetComponent<UnpoweredWireStat>().enabled = true;
            expPurples.gameObject.GetComponent<UnpoweredWireStat>().enabled = true;
        }else
        {
            expBlues.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
            expGreens.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
            expYellows.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
            expReds.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
            expPurples.gameObject.GetComponent<UnpoweredWireBehaviour>().enabled = false;
            expBlues.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
            expGreens.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
            expYellows.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
            expReds.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
            expPurples.gameObject.GetComponent<UnpoweredWireStat>().enabled = false;
        }
    }
    public void InteractuarBlue(){
        turnOffBlue.gameObject.SetActive(!turnOffBlue.gameObject.activeSelf);
        blue=!blue;
        turnOffYellow.gameObject.SetActive(!turnOffYellow.gameObject.activeSelf);
        yellow=!yellow;
        turnOffRed.gameObject.SetActive(!turnOffRed.gameObject.activeSelf);    
        red=!red;    
    }
    public void InteractuarYellow(){
        turnOffBlue.gameObject.SetActive(!turnOffBlue.gameObject.activeSelf);
        blue=!blue;
    }
    public void InteractuarPurple(){
        turnOffBlue.gameObject.SetActive(!turnOffBlue.gameObject.activeSelf);
        blue=!blue;
        turnOffRed.gameObject.SetActive(!turnOffRed.gameObject.activeSelf);
        red=!red;    
        turnOffGreen.gameObject.SetActive(!turnOffGreen.gameObject.activeSelf);
        green=!green;
    }
    public void InteractuarRed(){
        turnOffBlue.gameObject.SetActive(!turnOffBlue.gameObject.activeSelf);
        blue=!blue;
        turnOffPurple.gameObject.SetActive(!turnOffPurple.gameObject.activeSelf);
        purple=!purple;
        turnOffGreen.gameObject.SetActive(!turnOffGreen.gameObject.activeSelf);
        green=!green;
    
    }
    public void InteractuarGreen(){
        turnOffBlue.gameObject.SetActive(!turnOffBlue.gameObject.activeSelf);
        blue=!blue;
        turnOffRed.gameObject.SetActive(!turnOffRed.gameObject.activeSelf);
        red=!red;    
        turnOffGreen.gameObject.SetActive(!turnOffGreen.gameObject.activeSelf);
        green=!green;
        turnOffYellow.gameObject.SetActive(!turnOffYellow.gameObject.activeSelf);
        yellow=!yellow;    
    }
}

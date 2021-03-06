using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static bool active;
    public GameObject panel;
    static PlayerInputs inputs;
    void Start()
    {
       inputs = PlayerManager.instance.gameObject.GetComponent<PlayerInputs>();
       active = false;
       panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //active = !active;
            HandleActive(!active);
            panel.SetActive(active);
            //Time.timeScale = (active) ? 0 : 1f;
            //inputs.enabled = !active;
        }
    }
    
    public static void PauseGame()
    {
        HandleActive(true);
    }

    public static void ResumeGame()
    {
        HandleActive(false);
    }

    static void HandleActive(bool value)
    {
        active = value;
        Time.timeScale = (active) ? 0 : 1f;
        
        if (inputs != null)
        {
            inputs.enabled = !active;
        }
    }
}

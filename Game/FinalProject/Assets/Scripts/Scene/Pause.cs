using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool active;
    public GameObject panel;
    PlayerInputs inputs;
    void Start()
    {
       inputs = PlayerManager.instance.gameObject.GetComponent<PlayerInputs>();
       active = false;
       panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            active = !active;
            panel.SetActive(active);
            Time.timeScale = (active) ? 0 : 1f;
            inputs.enabled = !active;
        }
    }
}

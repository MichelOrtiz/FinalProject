using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start() {
        Destroy(PlayerManager.instance?.gameObject);
        Destroy(Inventory.instance?.gameObject);
        Destroy(CameraFollow.instance?.gameObject);
    }
    public void QuitGame(){
        Application.Quit();
    }
}

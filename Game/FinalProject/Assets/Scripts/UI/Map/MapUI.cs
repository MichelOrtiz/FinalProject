using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapUI : MonoBehaviour
{
    List<MapSlot> mapitas;

    public GameObject mapUI;
    // Start is called before the first frame update
    void Start()
    {
        mapUI.SetActive(true);
        mapitas = GetComponentsInChildren<MapSlot>().ToList<MapSlot>();
        SceneController.instance.SceneChanged += CambioEscena;
        CambioEscena();
        mapUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.inputs.OpenMap)
        {
            mapUI.SetActive(!mapUI.activeSelf);
        }
    }

    void CambioEscena(){
        mapUI.SetActive(true);
        foreach (MapSlot m  in mapitas)
        {
            m.UpdateUI();
            //m.isHere = SceneController.instance.currentScene == m.Scene;
        }
        mapUI.SetActive(false);
    }
}

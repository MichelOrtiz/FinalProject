using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapUI : MonoBehaviour
{
    List<MapSlot> mapitas;

    // Start is called before the first frame update
    void Start()
    {

        mapitas = GetComponentsInChildren<MapSlot>().ToList<MapSlot>();
        SceneController.instance.SceneChanged += CambioEscena;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CambioEscena(){
        foreach (MapSlot m  in mapitas)
        {
            m.UpdateUI();
            m.isHere = SceneController.instance.currentScene == m.Scene;
        }
    }
}

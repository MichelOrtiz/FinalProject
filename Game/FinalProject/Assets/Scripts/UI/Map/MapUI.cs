using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapUI : MonoBehaviour
{
    public List<MapSlot> mapitas;
    public GameObject mapUI;

    // Start is called before the first frame update
    void Start()
    {
        mapUI.SetActive(true);
        mapitas = GetComponentsInChildren<MapSlot>().ToList<MapSlot>();
        SceneController.instance.SceneChanged += CambioEscena;
        CambioEscena();
        loadLoadStuff();
        mapUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PlayerManager.instance.inputs.Map) && !FindObjectOfType<Pause>().panel.activeSelf)
        {
            mapUI.SetActive(!mapUI.activeSelf);
            if (mapUI.activeSelf)
            {
                //Pause.PauseGame();
            }else
            {
                Pause.ResumeGame();
            }
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

    void loadLoadStuff(){
        int[] scenes = {12,17,9,14,8,11,10,16,6,15,1,7,5,2,3,13,4};
        int wState = 200;
        List<WorldState> currentStates = SaveFilesManager.instance.currentSaveSlot.WorldStates;
        foreach (MapSlot slot in mapitas)
        {
            for (int index = 0; index < scenes.Length; index++)
            {
                if (slot.Scene == scenes[index])
                {
                    wState = 200+index;
                    foreach (WorldState w in currentStates)
                    {
                        if (w.id == wState && w.state)
                        {
                            slot.isObtained = true;
                            slot.UpdateUI();
                            break;
                        }
                    }
                }
            }
        }
    }
}

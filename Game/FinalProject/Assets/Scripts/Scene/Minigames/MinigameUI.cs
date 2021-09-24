using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameUI : MonoBehaviour
{
    public static MinigameUI instance;
    public GameObject background;
    public GameObject canvas;
    private GameObject currentMinigame;
    public TimerBar timerBar;
    public int rewardMoney;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void EndMinigame(float waitTime){
        Inventory.instance.AddMoney(rewardMoney);

        //var monos = ScenesManagers.GetComponentsInChildrenList<MonoBehaviour>(currentMinigame);
        var monos = currentMinigame.GetComponentsInChildren<MonoBehaviour>();

        if (monos != null)
        {
            var monosArray = ScenesManagers.ArrayToList<MonoBehaviour>(monos);
            monosArray.RemoveAll(m => (m is Image) || m is TMPro.TextMeshProUGUI || m is Text || m is Button);
            monosArray.ForEach(m => m.enabled = false);
        }

        Invoke("DestroyMinigame", waitTime);
    }


    public void RecieveMinigame(GameObject minigame){
        currentMinigame = Instantiate(minigame, new Vector3(0, 0, 90), Quaternion.identity);
        currentMinigame.transform.SetParent(canvas.transform, false);
        background.SetActive(true);
    }

    void DestroyMinigame()
    {
        if(currentMinigame != null)
        {
            background.SetActive(false);
            Destroy(currentMinigame);
        }
    }

}
using UnityEngine;
public class CatchTheFruitManager : MonoBehaviour
{
    [SerializeField] private loadlevel loadlevel;
    [SerializeField] private Spawner spawner;
    [SerializeField] private PopUpTrigger startPopUpTrigger;
    [SerializeField] private PopUpTrigger endPopUpTrigger;
    public static CatchTheFruitManager instance;

    public bool minigameEnded;


    byte initialMoney;


    void Awake()
    {
        if(instance!=null) return;
        instance = this;


        endPopUpTrigger.popUpUI.closedPopUp += ReturnToLastScene;

    }


    void Start()
    {
        startPopUpTrigger.TriggerPopUp(true);
        PlayerManager.instance.transform.position = loadlevel.loadPosition.position;
        loadlevel.gameObject.SetActive(false);


        initialMoney = (byte)Inventory.instance.GetMoney();

        
    }


    public void EndMinigame()
    {
        //loadlevel.gameObject.SetActive(true);
        spawner.gameObject.SetActive(false);
        

        byte moneyEarned = (byte)(Inventory.instance.GetMoney() - initialMoney);
        endPopUpTrigger.popUp.Message += moneyEarned + "G";
        endPopUpTrigger.TriggerPopUp(true);

        minigameEnded = true;
    }

    void ReturnToLastScene()
    {
        SceneController.instance.altDoor = loadlevel.noDoor;
        SceneController.instance.LoadScene(loadlevel.iLevelToLoad);
    }
}
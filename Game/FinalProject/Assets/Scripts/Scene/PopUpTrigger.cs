using UnityEngine;
public class PopUpTrigger : MonoBehaviour
{
    public PopUp popUp;
    public PopUpUI popUpUI;
    public void TriggerPopUp(bool pauseGame)
    {
        popUpUI.SetTitle(popUp.Title).SetMessage(popUp.Message).Show(pauseGame);
    }
}
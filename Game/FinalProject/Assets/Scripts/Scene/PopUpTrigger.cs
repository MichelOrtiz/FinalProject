using UnityEngine;
public class PopUpTrigger : MonoBehaviour
{
    public PopUp popUp;
    public void TriggerPopUp(bool pauseGame)
    {
        PopUpUI.Instance.SetTitle(popUp.Title).SetMessage(popUp.Message).Show(pauseGame);
    }
}
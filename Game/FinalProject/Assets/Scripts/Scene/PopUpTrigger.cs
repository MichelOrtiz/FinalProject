using UnityEngine;
public class PopUpTrigger : MonoBehaviour
{
    public PopUp popUp;
    [SerializeField] private PopUpUI popUpUI;
    public void TriggerPopUp(bool pauseGame)
    {
        popUpUI.SetTitle(popUp.Title).SetMessage(popUp.Message).Show(pauseGame);
    }
}
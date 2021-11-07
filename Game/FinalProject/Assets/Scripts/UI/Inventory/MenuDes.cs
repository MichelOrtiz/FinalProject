using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MenuDes : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] Transform holderButtons;
    List<Button> buttons;
    public delegate void OnUse();
    public OnUse onUse;
    public delegate void OnMove();
    public OnMove onMove;
    public delegate void OnLeave();
    public OnLeave onLeave;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<Button>();
        buttons = holderButtons.GetComponentsInChildren<Button>().ToList<Button>();
    }

    public void OnUseBtn(){
        onUse?.Invoke();
        gameObject.SetActive(false);
        onUse = null;
    }
    public void OnMoverBtn(){
        onMove?.Invoke();
        gameObject.SetActive(false);
        onMove = null;
    }
    public void OnLeaveBtn(){
        onLeave?.Invoke();
        gameObject.SetActive(false);
        onLeave = null;
    }
    public void OnPointerExit(PointerEventData data){
        gameObject.SetActive(false);
        onUse = null;
        onMove = null;
        onLeave = null;
    }
}

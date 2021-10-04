using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{
    public GameObject navi;
    public GameObject optionsMenu;
    private static KeybindManager instance;
    public  static KeybindManager MyInstance{
        get{
            if (instance==null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }
            return instance;
        }
    }
    
    public Dictionary<string, KeyCode> controlbinds {get; private set;}
    
    private string bindName;
    void Start()
    {
        controlbinds = new Dictionary<string, KeyCode>();
        
        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
        uiManager.keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        ResetBindValues();
        foreach(GameObject obj in uiManager.keybindButtons){
            Button b = obj.GetComponentInChildren<Button>();
            if(b != null){
                b.onClick.AddListener(delegate(){OnClickGetBinding(b.name);});
            }
        }

    }//KeybindManager.MyInstance.binds[""]

    public void BindKey(string key, KeyCode keyBind){
        Dictionary<string, KeyCode> currentDictionary = controlbinds;
        
        if (!currentDictionary.ContainsValue(keyBind))
        {
            if(currentDictionary.ContainsKey(key)){
                currentDictionary[key] = keyBind;
            }else{
                currentDictionary.Add(key, keyBind);
            }
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
            return;
        }else if(currentDictionary.ContainsValue(keyBind))
        {
            string prevKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[prevKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(prevKey, KeyCode.None);
            currentDictionary[key] = keyBind;
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
            return;
        }
        /*
        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        updateKey = string.Empty;
        */
    }
    
    string updateKey = null;
    KeyCode updateKeyCode = KeyCode.None;
    bool isUpdatingKey = false;
    public void OnClickGetBinding(string key){
        updateKey = key;
        isUpdatingKey = true;
    }

    void Update()
    {
        if(isUpdatingKey){
            updateKeyCode = GetKeyOnKeyBoard();
            if(updateKeyCode != KeyCode.None){
                BindKey(updateKey,updateKeyCode);
                isUpdatingKey = false;
                updateKeyCode = KeyCode.None;
            }
        }
    }
    KeyCode GetKeyOnKeyBoard(){
        foreach(KeyCode k in Enum.GetValues(typeof(KeyCode))){
            if(Input.GetKeyDown(k)){
                return k;
            }
        }
        return KeyCode.None;
    }

    public void ExitUI(){
        if(!controlbinds.ContainsValue(KeyCode.None))
        {
            gameObject.SetActive(false);
            optionsMenu.SetActive(true);
        }else{
            if(navi != null){
                StartCoroutine(NaviIsAnnoying());
            }
        }
    }
    public void ResetBindValues(){
        //MOVEMENT
        BindKey("MOVEUP", KeyCode.W);
        BindKey("MOVEDOWN", KeyCode.S);
        BindKey("MOVERIGHT", KeyCode.D);
        BindKey("MOVELEFT", KeyCode.A);
        BindKey("MOVEJUMP", KeyCode.Space);
        //MENU
        BindKey("MENUPAUSE", KeyCode.Escape);
        BindKey("MENUMINIMAP", KeyCode.Tab);
        BindKey("MENUINVENTORY", KeyCode.I);
        BindKey("MENUINTERACTION", KeyCode.E);
        //ACTION 
        BindKey("RUN", KeyCode.LeftShift);
        BindKey("OBJ1", KeyCode.Q);
        BindKey("OBJ2", KeyCode.R);
        BindKey("FOOD1", KeyCode.Alpha1);
        BindKey("FOOD2", KeyCode.Alpha2);
        BindKey("FOOD3", KeyCode.Alpha3);
        BindKey("FOOD4", KeyCode.Alpha4);
        BindKey("FOOD5", KeyCode.Alpha0);
        BindKey("SKILL1", KeyCode.Alpha5);
        BindKey("SKILL2", KeyCode.Alpha6);
        BindKey("SKILL3", KeyCode.Alpha7);
        BindKey("SKILL4", KeyCode.Alpha8);
        BindKey("SKILL5", KeyCode.Alpha9);
    }

    IEnumerator NaviIsAnnoying(){
            navi.SetActive(true);
            yield return new WaitForSeconds(2f);
            navi.SetActive(false);
    }
}

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
    public GameObject keybindUI;
    public static KeybindManager instance;
    
    private void Awake() {
        if(instance != null){
            Destroy(this);
        }else{
            instance = this;
        }
    }
    
    public Dictionary<string, KeyCode> controlbinds {get; set;}
    
    private string bindName;
    void Start()
    {
        controlbinds = new Dictionary<string, KeyCode>();
        if(FindObjectOfType<Pause>() != null){
            FindObjectOfType<Pause>().panel.SetActive(true);
        }
        keybindUI.SetActive(true);
        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
        uiManager.keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        SaveFilesManager saveFilesManager = SaveFilesManager.instance;
        if(saveFilesManager != null){
            if(saveFilesManager.currentSaveSlot != null){
                if(saveFilesManager.currentSaveSlot.controlBindsKeys != null){
                    //Debug.Log("Cargando keybinds");
                    controlbinds = new Dictionary<string, KeyCode>();
                    List<string> keys = saveFilesManager.currentSaveSlot.controlBindsKeys;
                    List<KeyCode> values = saveFilesManager.currentSaveSlot.controlBindsValues;
                    foreach(string key in keys){
                        int i = keys.IndexOf(key);
                        //Debug.Log(key + "-" + values[i].ToString());
                        BindKey(key, values[i]);
                    }
            }else{
                ResetBindValues();
                //saveFilesManager.currentSaveSlot.controlbinds = controlbinds;
                saveFilesManager.currentSaveSlot.controlBindsKeys = KeybindManager.instance.controlbinds.Keys.ToList<string>();
                saveFilesManager.currentSaveSlot.controlBindsValues = KeybindManager.instance.controlbinds.Values.ToList<KeyCode>();
            }
            }else{
                ResetBindValues();
            }
            
        }else{
            ResetBindValues();
        }

        foreach(GameObject obj in uiManager.keybindButtons){
            Button b = obj.GetComponentInChildren<Button>();
            if(b != null){
                b.onClick.AddListener(delegate(){OnClickGetBinding(b.name);});
            }
        }
        
        keybindUI.SetActive(false);
        if(FindObjectOfType<Pause>() != null){
            FindObjectOfType<Pause>().panel.SetActive(false);
        }
        if(FindObjectOfType<PlayerInputs>() != null){
            FindObjectOfType<PlayerInputs>().controlBinds = controlbinds;
            FindObjectOfType<PlayerInputs>().Pause = controlbinds["MENUPAUSE"];
            FindObjectOfType<PlayerInputs>().Map = controlbinds["MENUMAP"];
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
            keybindUI.SetActive(false);
            optionsMenu.SetActive(true);
        }else{
            if(navi != null){
                StartCoroutine(NaviIsAnnoying());
            }
        }
        if(FindObjectOfType<PlayerInputs>() != null){
            FindObjectOfType<PlayerInputs>().controlBinds = controlbinds;
            FindObjectOfType<PlayerInputs>().Pause = controlbinds["MENUPAUSE"];
            FindObjectOfType<PlayerInputs>().Map = controlbinds["MENUMAP"];
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
        BindKey("MENUMAP", KeyCode.M);
        BindKey("MENUMINIMAP", KeyCode.Tab);
        BindKey("MENUINVENTORY", KeyCode.Mouse1);
        BindKey("MENUINTERACTION", KeyCode.E);
        BindKey("MENUFASTASSIGN", KeyCode.LeftControl);
        //ACTION 
        BindKey("RUN", KeyCode.LeftShift);
        BindKey("OBJ1", KeyCode.Q);
        BindKey("OBJ2", KeyCode.R);
        BindKey("FOOD1", KeyCode.Alpha1);
        BindKey("FOOD2", KeyCode.Alpha2);
        BindKey("FOOD3", KeyCode.Alpha3);
        BindKey("FOOD4", KeyCode.Alpha4);
        BindKey("FOOD5", KeyCode.Alpha5);
        BindKey("SKILL1", KeyCode.Alpha6);
        BindKey("SKILL2", KeyCode.Alpha7);
        BindKey("SKILL3", KeyCode.Alpha8);
        BindKey("SKILL4", KeyCode.Alpha9);
        BindKey("SKILL5", KeyCode.Alpha0);
    }

    IEnumerator NaviIsAnnoying(){
            navi.SetActive(true);
            yield return new WaitForSeconds(2f);
            navi.SetActive(false);
    }
    public static readonly string[] defaultKeys = {"MOVEUP","MOVEDOWN","MOVERIGHT","MOVELEFT","MOVEJUMP","MENUPAUSE","MENUMAP","MENUMINIMAP","MENUINVENTORY","MENUINTERACTION","MENUFASTASSIGN","RUN","OBJ1","OBJ2","FOOD1","FOOD2","FOOD3","FOOD4","FOOD5","SKILL1","SKILL2","SKILL3","SKILL4","SKILL5"};
    public static readonly KeyCode[] defaultValues = {KeyCode.W,KeyCode.S,KeyCode.D,KeyCode.A,KeyCode.Space,KeyCode.Escape,KeyCode.M,KeyCode.Tab,KeyCode.Mouse1,KeyCode.E,KeyCode.LeftControl,KeyCode.LeftShift,KeyCode.Q,KeyCode.R,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,KeyCode.Alpha0};
}

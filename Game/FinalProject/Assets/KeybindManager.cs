using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
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
    
    public Dictionary<string, KeyCode> Movebinds {get; private set;}
    public Dictionary<string, KeyCode> Menubinds {get; private set;}
    public Dictionary<string, KeyCode> Skillbinds {get; private set;}
    
    private string bindName;
    void Start()
    {
        Skillbinds = new Dictionary<string, KeyCode>();
        Menubinds = new Dictionary<string, KeyCode>();
        Movebinds = new Dictionary<string, KeyCode>();
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
        BindKey("SKILL1", KeyCode.Alpha5);
        BindKey("SKILL2", KeyCode.Alpha6);
        BindKey("SKILL3", KeyCode.Alpha7);
        BindKey("SKILL4", KeyCode.Alpha8);
        BindKey("SKILL5", KeyCode.Alpha9);
        BindKey("SKILL5", KeyCode.Alpha0);
    }

    public void BindKey(string key, KeyCode keyBind){
        Dictionary<string, KeyCode> currentDictionary = Skillbinds;
        if (key.Contains("MENU"))
        {
            currentDictionary = Menubinds;
        }
        if (key.Contains("MOVE"))
        {
            currentDictionary = Movebinds;
        }
        if (!currentDictionary.ContainsValue(keyBind))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
        }else if(currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            //UIManager.MyInstance.UpdateKeyText(key, keyCode.None);
        }
        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }

    void Update()
    {
        
    }
}

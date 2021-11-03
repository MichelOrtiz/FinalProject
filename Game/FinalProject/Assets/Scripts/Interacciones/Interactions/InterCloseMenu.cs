using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/CloseMenu")]
public class InterCloseMenu : Interaction
{
    private enum Menu {Inventario,Cofre,Mapa,Habilides,Nada}
    [SerializeField] Menu closeMenu = Menu.Nada;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                CloseMenu();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            CloseMenu();
        }
    }
    void CloseMenu(){
        switch(closeMenu){
            case Menu.Inventario:{
                Debug.Log("Cerrando inv");
                InventoryUI.instance.UI.SetActive(false);
                break;
            }
            case Menu.Cofre:{
                CofreUI.instance.cofreUI.SetActive(false);
                break;
            }
            case Menu.Habilides:{
                AbilityUI.instance.UI.SetActive(false);
                break;
            }
            case Menu.Mapa:{
                MapUI.instance.mapUI.SetActive(false);
                break;
            }
            case Menu.Nada:
            default:{
                Debug.Log("No se asigno un menu para abrir");
                break;
            }
        }
        onEndInteraction?.Invoke();
    }
    
}

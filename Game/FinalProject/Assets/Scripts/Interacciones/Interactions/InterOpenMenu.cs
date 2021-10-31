using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/OpenMenu")]
public class InterOpenMenu : Interaction
{
    private enum Menu {Inventario,Cofre,Mapa,Habilides,Nada}
    [SerializeField] Menu openMenu = Menu.Nada;
    public override void DoInteraction()
    {
        if(condition != null){
            if(condition.isDone){
                OpenMenu();
            }else{
                onEndInteraction?.Invoke();
            }
        }else{
            OpenMenu();
        }
    }
    void OpenMenu(){
        switch(openMenu){
            case Menu.Inventario:{
                InventoryUI.instance.UI.SetActive(true);
                break;
            }
            case Menu.Cofre:{
                CofreUI.instance.cofreUI.SetActive(true);
                break;
            }
            case Menu.Habilides:{
                AbilityUI.instance.UI.SetActive(true);
                break;
            }
            case Menu.Mapa:{
                MapUI.instance.mapUI.SetActive(true);
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

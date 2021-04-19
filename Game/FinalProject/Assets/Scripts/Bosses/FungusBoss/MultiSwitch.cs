using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSwitch : MonoBehaviour
{
    public int numberOfSwitch;
    public bool jugando;
    [SerializeField] private List<Switch> Orden;
    [SerializeField] private List<bool> OrdenJugador;
    
    public Door door;
    private PlayerManager player;
    [SerializeField] private List<Switch> switches;

    
    void Start()
    {
        OrdenJugador = new List<bool>(Orden.Count);
        for (int i = 0; i < Orden.Count; i++)
        {
            OrdenJugador.Add(false);
        }
        Default();

        /*allDoors = ScenesManagers.GetObjectsOfType<Door>();*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!jugando)
        {
            jugando = Jugando();
        }else
        {
            for (int i = 0; i < switches.Count; i++)
            {
                
                if ((switches[i].activado && OrdenJugador[i] == Orden[i].activado))
                {
                    OrdenJugador[i] = true;//
                    
                }else if(OrdenJugador[i] != Orden[i])
                {
                    Default();
                    break;
                }
                /*if (!OrdenJugador[i] && switches[i].activado )
                {
                    OrdenJugador[i] = true;
                    if (i!=Orden[i])
                    {
                        Default();
                    }
                    break;
                }*/
            }            
        }
        if (Activados()&& !door.isOpen)
        {
            door.Activate();
        }
        
    }

    void Default(){
        foreach (var Switch in switches)
        {
            Switch.activado = false;
        }
        for (int i = 0; i < OrdenJugador.Count; i++)
        {
            OrdenJugador[i] = false;
        }
        jugando = false;
    }
    bool Jugando(){
        return switches.Exists(s=>s.activado);
    }
    bool Activados(){
        foreach (var Switch in switches)
        {
            if (!Switch.activado)
            {
                return false;
            }
        }
        return true;
    }
}
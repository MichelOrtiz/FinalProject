using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class password : MasterMinigame
{
    public int direccion, cant, index;
    public string x;
    public List<DireccionMiniJuegoMeduf> DireccionMiniJuegos; 
    public List<DireccionMiniJuegoMeduf> ContraCorrect;
    public List<DireccionMiniJuegoMeduf> ContraUser;
    [SerializeField] ArrayList contraseña;

    public delegate void PasswordSet();
    public event PasswordSet PasswordSetHandler;
    [SerializeField] private MBPasswordView passwordView;
    void Start()
    {
        cant = RandomGenerator.NewRandom(5,10);
        for (int i = 0; i < cant; i++)
        {
            ContraCorrect.Add((DireccionMiniJuegoMeduf)(int)UnityEngine.Random.Range(0,Enum.GetValues(typeof(DireccionMiniJuegoMeduf)).Length));
        }
        passwordView.SetImages(ContraCorrect);
    }

    // Update is called once per frame
    void Update()
    {
        if (index == ContraCorrect.Count)
        {
            Debug.Log("Ganaste :3");
            index = 0;
            OnWinMinigame();
        }
    }
    void OnMouseDown(){
         
    }
    public void AddPressedCheck(DireccionMiniJuegoMeduf direccion){
        if (index < ContraCorrect.Count)
        {
            ContraUser.Add(direccion);
            if (ContraUser[index] == ContraCorrect[index])
            {
                index++;    
            }else
            {
                ContraUser.Clear();
                index=0;
            }
            switch (direccion)
            {
                case DireccionMiniJuegoMeduf.Arriba:
                    x="Arriba";
                    cant++;
                break;
                case DireccionMiniJuegoMeduf.Derecha:
                    x="Derecha";
                    cant++;
                break;
                case DireccionMiniJuegoMeduf.Izquierda:
                    x="Izquierda";
                    cant++;
                break;    
                case DireccionMiniJuegoMeduf.Abajo:
                    x="Abajo";
                    cant++;
                break;
            } 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSesame : MonoBehaviour
{
    Animator animator;
    public float rango = 3f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(PlayerManager.instance.GetPosition(),transform.position);
        if(distance <= rango){
            animator.Play("Cofre_open");
            PlayerManager.instance.inputs.Interact -= OpenCofre;
            PlayerManager.instance.inputs.Interact += OpenCofre;
        }else{
            animator.Play("Cofre_close");
            PlayerManager.instance.inputs.Interact -= OpenCofre;
            CofreUI.instance.cofreUI.SetActive(false);
        }
    }
    void OpenCofre(){
        CofreUI.instance.cofreUI.SetActive(!CofreUI.instance.cofreUI.activeSelf);
    }
}

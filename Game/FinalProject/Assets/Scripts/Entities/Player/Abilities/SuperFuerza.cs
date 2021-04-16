using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFuerza : Ability
{
    [SerializeField]private BoxCollider2D collider;
    //La idea que tengo es, hice una caja, con 2500 de masa, creo, que esas son las cajas que se moveran con la super fuerza, entonces
    //lo que debemos hacer es bajarle la masa a esa caja para que se pueda mover, pero no se hacer eso :3, en la zona de nieve esta la caja*/
    protected override void Update() {
        collider.enabled = isUnlocked;
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Movable")){
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Movable")){
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}

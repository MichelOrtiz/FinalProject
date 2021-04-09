using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]private float damage;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            PlayerManager.instance.TakeTirement(damage);
            Destroy(gameObject);
            return;
        }
        if(other.CompareTag("Ground")){
            Destroy(gameObject);
            return;
        }
    }
}

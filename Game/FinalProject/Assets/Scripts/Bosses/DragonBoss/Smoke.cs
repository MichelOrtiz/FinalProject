using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField]private int dmg;
    [SerializeField]private float dmgInterval;
    [SerializeField]private float speedMultiplier;
    [SerializeField]private Rigidbody2D rigidbody2d;
    [SerializeField]private Vector3 target;

    [SerializeField] private float lifeTime;
    private float curLifeTime;

    private Vector3 shootDir;
    private Vector3 startPosition;
    private float currentTime;
    private bool isPlayerInside;
    private void Start() {
        startPosition = transform.position;
        shootDir = (target - startPosition).normalized;
    }
    private void Update() {
        Movement();
        if(isPlayerInside){
            currentTime += Time.deltaTime;
            if(currentTime >= dmgInterval){
                PlayerManager.instance.TakeTirement(dmg);
                currentTime=0;
            }
        }
        else{
            currentTime=0;
        }

        if (curLifeTime > lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            curLifeTime += Time.deltaTime;
        }
    }
    void Movement(){
        transform.position += shootDir * speedMultiplier * Time.deltaTime *(rigidbody2d.gravityScale != 0? rigidbody2d.gravityScale : 1);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))isPlayerInside=true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))isPlayerInside=false;
    }
    public void SetTarget(Vector3 newTarget){
        target=newTarget;
        shootDir = (target - startPosition).normalized;
    }
}

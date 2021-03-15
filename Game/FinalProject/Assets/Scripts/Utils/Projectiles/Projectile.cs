using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    
    // To see where the projectile will go to
    [SerializeField] private bool targetWarningAvailable; 
    [SerializeField] private GameObject warning;
    
    // Time until destroyed
    [SerializeField] private float waitTime;

    private Vector3 playerPosition;
    private Vector3 target;


    void Start()
    {
        speedMultiplier *= Entity.averageSpeed;
        playerPosition = PlayerManager.instance.GetPosition();
        target = playerPosition;
        if (targetWarningAvailable)
        {
            Instantiate(warning, target, Quaternion.identity);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedMultiplier * Time.deltaTime);
        if (transform.position == target)
        {
            // wait certain time until the object is destroyed
            if (waitTime <= 0)
            {
                DestroyProjectile();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DestroyProjectile();
        }
    }

    

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalBar : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float lifeTime;
    private float currentTime;
    private PlayerManager player;
    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.TakeTirement(damageAmount);
            player.SetImmune();
        }

    }
}

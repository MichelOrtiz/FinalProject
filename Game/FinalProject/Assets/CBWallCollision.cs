using System.Collections;
using System.Collections.Generic;
using FinalProject.Assets.Scripts.Utils.Sound;
using UnityEngine;

public class CBWallCollision : MonoBehaviour
{
    [Header("Projectile stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    [SerializeField] Transform shootPos;

    [Header("Hits")]
    [SerializeField] List<string> collisionTags;
    [SerializeField] private byte maxHits;
    private byte currentHits;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProjectileAttack()
    {
        return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionTags.Exists(c => c == other.tag))
        {
            if (currentHits < maxHits-1)
            {
                currentHits++;
            }
            else
            {
                //ShotProjectile(shootPos, other.gameObject.transform.position);
                AudioManager.instance?.Play("FallWall");
                projectileShooter.ShootProjectile(other.transform.position);
                currentHits = 0;
            }
        }
    }
}

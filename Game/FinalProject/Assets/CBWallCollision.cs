using System.Collections.Generic;
using FinalProject.Assets.Scripts.Utils.Sound;
using UnityEngine;

public class CBWallCollision : MonoBehaviour
{
    [Header("Projectile stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;

    [Header("Hits")]
    [SerializeField] List<string> collisionTags;
    [SerializeField] private byte maxHits;
    private byte currentHits;

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

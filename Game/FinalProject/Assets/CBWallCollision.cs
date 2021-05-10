using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBWallCollision : MonoBehaviour, IProjectile
{
    [Header("Projectile stuff")]
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    [SerializeField] Transform shootPos;
    private bool shotProjectile;

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

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionTags.Exists(c => c == other.tag))
        {
            if (currentHits < maxHits-1)
            {
                currentHits++;
            }
            else if (!shotProjectile)
            {
                ShotProjectile(shootPos, other.gameObject.transform.position);
                shotProjectile = true;
            }
        }
    }
}

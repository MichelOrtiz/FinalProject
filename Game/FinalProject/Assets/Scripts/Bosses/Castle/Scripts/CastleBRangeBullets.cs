using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBRangeBullets : MonoBehaviour, IProjectile
{
    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    //[SerializeField] private Transform shotTarget;
    [SerializeField] private Transform shotPoint;
    
    private Vector2 shotTarget;
    [SerializeField] private float timeBtwShot;
    private float currentTimeBtwShot;
    #endregion

    #region TargetRangeStuff
    [Header("Target Range Stuff")]
    
    
    [SerializeField] private float radius;
    
    #endregion

    private PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeBtwShot > timeBtwShot)
        {
            ShotProjectiles();
            currentTimeBtwShot = 0;
        }
        else
        {
            currentTimeBtwShot += Time.deltaTime;
        }
    }

    public void ShotProjectiles()
    {
        Vector2 playerPosition = player.GetPosition();
        
        float x = Random.Range(playerPosition.x - radius, playerPosition.x + radius);
        float y = Random.Range(playerPosition.y - radius, playerPosition.y + radius);

        shotTarget = playerPosition + (Vector2) transform.InverseTransformPoint(new Vector2(x, y)) ;

        ShotProjectile(shotPoint, shotTarget);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }
}

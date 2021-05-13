using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBBatGenerator : MonoBehaviour, IProjectile
{
    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private Transform shotPos;
    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    [SerializeField] private float timeBtwBat;
    private float curTimeBtwBat;
    [SerializeField] private int batPerShot;
    private int batsShot;
    private bool shooting;
    #endregion

    #region PlayerEtc
    [Header("Player, position, etc")]
    [SerializeField] private Space space;
    [SerializeField] private Vector2 direction;
    //private Vector2 directionCheck;
    [SerializeField] private float minXDistanceCheck;
    [SerializeField] private float yOffsetCheck;
    [SerializeField] private State effectOnPlayer;
    private PlayerManager player;
    private float playerYPos;
    private float distanceFromY;
    private float distanceFromX;

    private RaycastHit2D raycast;
    #endregion

    void Start()
    {
        player = PlayerManager.instance;
        if (space == Space.Self)
        {
            direction += (Vector2) transform.position;
        }
        else
        {
            direction = transform.TransformDirection(direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromY = Mathf.Abs(player.GetPosition().y - transform.position.y);
        distanceFromX = Mathf.Abs(player.GetPosition().x - transform.position.x);
        if (distanceFromY <= yOffsetCheck && distanceFromX >= minXDistanceCheck || batsShot > 0)
        {
            if (batsShot < batPerShot)
            {
                if (curTimeBtwBat > timeBtwBat)
                {
                    
                    ShotProjectile(shotPos, direction);
                    batsShot++;
                    curTimeBtwBat = 0;
                }
                else
                {
                    curTimeBtwBat += Time.deltaTime;
                }
            }
        }
        else
        {
            batsShot = 0;
        } 
    }


    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
        if (effectOnPlayer != null)
        {
            player.statesManager.AddState(effectOnPlayer);
        }
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.position, transform.rotation).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hawk : Entity, ILaser
{
    [Header("Speed Params")]
    [SerializeField] private float baseSpeedMultiplier;
    [SerializeField] private float speedChangeMultiplier;
    private float speed;
    [SerializeField] private float intervalToChangeSpeed;
    private float currentTimeUntilChange;
    [SerializeField] private float changeSpeedTimeAcive;
    private float currentChangedSpeedTime;
    private bool speedChanged;

    #region LaserBeam
    [Header("Laser Beam")]
    [SerializeField] private Transform shotPos;
    public Transform ShotPos { get => shotPos; }
    private Vector2 endPos;
    public Vector2 EndPos { get => endPos; } 
    [SerializeField] private float intervalToShoot;
    private float timeToShoot;

    //[SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject laserPrefab;
    private Laser laser;
    [SerializeField] private float laserDamage;
    [SerializeField] private float laserSpeed;

    #endregion

    

    [SerializeField] private float damageAmount;

    /* change to enemy and inherit later*/
    private EnemyCollisionHandler collisionHandler;
    private bool touchingPlayer;

    private PlayerManager player;
    /*-----------------------------------*/


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player = PlayerManager.instance;
        speed = averageSpeed * baseSpeedMultiplier;
    }

    // Update is called once per frame
    new void Update()
    {
        HandleSpeed();
        HandleLaser();
        base.Update();
    }

    void FixedUpdate()
    {
        if (GetPosition().y > player.GetPosition().y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }


    void HandleSpeed()
    {
        if (currentTimeUntilChange > intervalToChangeSpeed)
        {
            if (!speedChanged)
            {
                speed = averageSpeed * speedChangeMultiplier;
                speedChanged = true;
            }
            if (currentChangedSpeedTime > changeSpeedTimeAcive)
            {
                speed = averageSpeed * baseSpeedMultiplier;
                currentChangedSpeedTime = 0;
                currentTimeUntilChange = 0;
                speedChanged = false;
            }
            else
            {
                currentChangedSpeedTime += Time.deltaTime;
            }
        }
        else
        {
            currentTimeUntilChange += Time.deltaTime;
        }
    }

    void HandleLaser()
    {
        if (timeToShoot > intervalToShoot)
        {
            Vector2 newShotPosition =  new Vector2(player.GetPosition().x, shotPos.position.y);
            shotPos.position = newShotPosition;
            // SetPositionAndRotation(newShotPosition, Quaternion.identity);

            endPos = player.GetPosition();
            ShootLaser(shotPos.position, EndPos);

            timeToShoot = 0;
        }
        else
        {
            timeToShoot += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = true;
            player.TakeTirement(damageAmount);
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    public void ShootLaser(Vector2 from, Vector2 to)
    {
        laser = Instantiate(laserPrefab, from, Quaternion.identity).GetComponent<Laser>();
        laser.Setup(from, to, laserSpeed, this);
    }

    public void LaserAttack()
    {
        player.TakeTirement(laserDamage);
    }
}

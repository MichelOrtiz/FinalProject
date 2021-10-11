using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class WallCristal : Enemy
{
    [Header("Self Additions")]
    [Header("Flash")]
    private FlashImage flashImage;
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashTime;
    [Range(0,1)]
    [SerializeField] private float minAlpha;
    [Range(0,1)]
    [SerializeField] private float maxAlpha;

    [Header("Projectile")]
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;
    private bool projectileShot;
    private bool projectileTouchedPlayer;

    [Header("Sprite Change")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteWhenChase;
    private Sprite defaultSprite;

    private List<Transform> positions;
    private Vector2 spawnedPos;

    ProbabilitySpawner spawner;
    new void Awake()
    {
        base.Awake();
        defaultSprite = spriteRenderer.sprite;
    }

    new void Start()
    {
        base.Start();
        flashImage = FindObjectOfType<FlashImage>();
        projectileShooter.ProjectileTouchedPlayerHandler += projectileShooter_ProjectileTouchedPlayer;
        flashImage.FlashInCompleteHandler += flashImage_OnFlashInComplete;

        spawner = GameObject.FindWithTag("EnemySpawner")?.GetComponent<ProbabilitySpawner>();
        ProbabilitySpawn spawn = spawner?.SpawnedObjects.Find(g => g.gameObject == gameObject).probabilitySpawn; 
        if (spawn != null)
        {
            positions = spawn.positions;
            
            var instantiated = spawner.SpawnedObjects.Find(s => s.gameObject == gameObject); 

            if ( instantiated != null )
            {
                spawnedPos = instantiated.spawnedPos;
            }
        }

    }
    
    new void Update()
    {
        if (groundChecker.isGrounded && rigidbody2d.velocity.magnitude != 0) enemyMovement.StopMovement();
        if (projectileShot && projectileShooter.Projectile == null && !projectileTouchedPlayer)
        {
            projectileShot = false;
        }
        base.Update();    
    }

    protected override void MainRoutine()
    {
        ChangeSprite(defaultSprite);
        curTimeBtwShot = 0;
    }

    protected override void ChasePlayer()
    {
        //ChangeSprite(spriteWhenChase);
        
        if (curTimeBtwShot > timeBtwShot)
        {
            if (!projectileShot)
            {
                projectileShooter.ShootProjectile(player.GetPosition());
                projectileTouchedPlayer = false;
                projectileShot = true;
                curTimeBtwShot = 0;
            }
        }
        else
        {
            curTimeBtwShot += Time.deltaTime;
        }
    }

    void ChangeSprite(Sprite sprite)
    {
        if (spriteRenderer.sprite != sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }
    protected override void groundChecker_Grounded(string groundTag)
    {
        base.groundChecker_Grounded(groundTag);
        collisionHandler.GetComponent<Collider2D>().isTrigger = false;
    }

    void projectileShooter_ProjectileTouchedPlayer()
    {
        flashImage.Flash(flashTime, minAlpha, maxAlpha);
        projectileTouchedPlayer = true;
    }
    void flashImage_OnFlashInComplete()
    {
        if (!projectileShot) return;
        if (positions.Count >= 1)
        {
            Vector3 newPosition;
            do
            {
                newPosition = RandomGenerator.RandomElement<Transform>(positions).position;
            }
            while(newPosition == (Vector3)spawnedPos);
            spawnedPos = newPosition;

            
            Vector3 playerNewPos;
            do
            {
                playerNewPos = RandomGenerator.RandomElement<Transform>(positions).position;
            }
            while(playerNewPos == newPosition);

            transform.position = newPosition;
            player.transform.position = playerNewPos;
            curTimeBtwShot = 0;
            projectileShot = false;
        }
    }
}
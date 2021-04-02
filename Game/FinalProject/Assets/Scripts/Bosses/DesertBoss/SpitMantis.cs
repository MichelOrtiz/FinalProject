using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitMantis : Mantis, IProjectile
{
    [SerializeField] private Transform shotProjectilePos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float startTimeBeforeShot;

    [SerializeField] private float startTimeAfterShot;
    [SerializeField] private List<Transform> targetPlatforms;
    [SerializeField] private int minChases;
    [SerializeField] private int maxChases;
    [SerializeField] private int chasesToDo;

    private Projectile projectile;
    public Transform targetPlatform;
    private float timeBeforeShot;
    private float timeAfterShot;
    private int chasesDone;
    private bool shot;

    new void Start()
    {
        chasesToDo = RandomGenerator.NewRandom(minChases, maxChases);
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if(shot)
        {
            if (timeAfterShot > startTimeAfterShot)
            {
                shot = false;
                timeAfterShot = 0;
            }
            else
            {
                timeAfterShot += Time.deltaTime;
            }
        }
        
        base.Update();
    }

    new void FixedUpdate()
    {
        if (!shot)
        {
            if (chasesDone < chasesToDo)
            {
                base.FixedUpdate();
            }
            else
            {
                if (timeBeforeShot > startTimeBeforeShot)
                {
                    targetPlatform = GetProjectileTarget();
                    targetPlatform.GetComponent<Platform>().isTarget = true;
                    Debug.Log(targetPlatform.gameObject.name + " " + targetPlatform.GetComponent<Platform>().isTarget);
                    ShotProjectile(shotProjectilePos, targetPlatform.position);
                    chasesToDo = RandomGenerator.NewRandom(minChases, maxChases);
                    timeBeforeShot = 0;
                    chasesDone = 0;
                }
                else
                {
                    timeBeforeShot += Time.deltaTime;
                }
            }
        }
        
    }

    protected override void MainRoutine()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ChasePlayer()
    {
        chasesDone++;
        
        base.ChasePlayer();
    }

    public void ProjectileAttack()
    {
        
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        shot = true;
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }

    private Transform GetProjectileTarget()
    {
        int chosenPlatform = RandomGenerator.NewRandom(0, targetPlatforms.Count - 1);
        return targetPlatforms[chosenPlatform];
    }
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Bosses.DesertBoss;
using UnityEngine;

public class SpitMantis : Mantis
{
    [Header ("Projectile")]
    [SerializeField] private ProjectileShooter projectileShooter;
    [SerializeField] private float startTimeBeforeShot;
    private float timeBeforeShot;
    [SerializeField] private float startTimeAfterShot;
    private float timeAfterShot;
    private bool shot;

    //[SerializeField] private List<GameObject> targetPlatforms;
    
    [Header ("Chases")]
    [SerializeField] private byte minChases;
    [SerializeField] private byte maxChases;
    [SerializeReference]private byte chasesToDo;
    [SerializeReference]private byte chasesDone;


    public GameObject targetPlatform;

    new void Start()
    {
        base.Start();

        chasesToDo = (byte)RandomGenerator.NewRandom(minChases, maxChases);
        
        /*foreach (var platform in targetPlatforms)
        {
            platform = ScenesManagers.GetObjectsOfType<Platform>().Find(p => p == platform);
        }*/
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
                chasesDone = 0;

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
        //if (!shot)
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
                    targetPlatform.GetComponent<Platform>().SetTarget();
                    projectileShooter.ShootProjectile(targetPlatform.transform.position);
                    shot = true;
                    chasesToDo = (byte)RandomGenerator.NewRandom(minChases, maxChases);
                    timeBeforeShot = 0;
                }
                else
                {
                    timeBeforeShot += Time.deltaTime;
                }
            }
        }
        
    }


    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        chasesDone++;
    }

    private GameObject GetProjectileTarget()
    {
        int chosenPlatform = RandomGenerator.NewRandom(0, ScenesManagers.FindObjectOfType<MantisBoss>().allPlatforms.Count -1);
        return ScenesManagers.FindObjectOfType<MantisBoss>().allPlatforms[chosenPlatform].gameObject;
    }
}

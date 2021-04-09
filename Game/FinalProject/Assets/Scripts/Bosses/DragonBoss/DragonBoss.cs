using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : MonoBehaviour,IProjectile
{
    [SerializeField]private Transform head;
    [SerializeField]private Transform tail;
    [SerializeField]private GameObject projectilePrefab;
    [SerializeField]private float shootInterval;
    [SerializeField]private int nProjectiles;
    private int currentProjectiles;
    [SerializeField]private float roarInterval;
    [SerializeField]private State roarEffect;
    [SerializeField]private FallingRocks fallingRocks;
    [SerializeField]private float fallingRockInterval;
    [SerializeField]private int minFallingRocks;
    [SerializeField]private int maxFallingRocks;
    private float rockTime;
    private int prevNRocks = 0;
    private int currentFallingRocks;
    private bool haveRocksFallen;
    [SerializeField]private GameObject smokePrefab;
    [SerializeField]private float smokeInterval;
    private PlayerManager player;
    private Projectile projectile;
    private float currentTime;
    

    void Start()
    {
        player = PlayerManager.instance;
        fallingRocks = FallingRocks.instance;
        currentProjectiles = 0;
        currentFallingRocks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        rockTime += Time.deltaTime;
        if(currentTime>=shootInterval && currentProjectiles<nProjectiles){
            currentTime=0;
            ShotProjectile(head,player.transform.position);
            currentProjectiles++;
        }
        if(currentTime>=roarInterval){
            currentTime=0;
            Roar();
            currentProjectiles=0;
        }
        if(maxFallingRocks>0 && minFallingRocks>0){
            if(rockTime>=fallingRockInterval && !haveRocksFallen){
                rockTime=0;
                haveRocksFallen = true;
                int n = RandomGenerator.NewRandom(minFallingRocks,maxFallingRocks);
                if(n==prevNRocks)n = RandomGenerator.NewRandom(minFallingRocks,maxFallingRocks);
                for(int i=0;i<n;i++)
                SwingTail();
            }
            if(rockTime>=smokeInterval && haveRocksFallen){
                rockTime = 0;
                haveRocksFallen = false;
                ShotSmoke();
            }
        }
        
    }
    public void ProjectileAttack()
    {
        player.TakeTirement(projectile.damage);
    }

    public void ShotProjectile(Transform from, Vector3 to)
    {
        projectile = Instantiate(projectilePrefab, from.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Setup(from, to, this);
    }
    void Roar(){
        //Debug.Log("ROARRR");
        if(player.isGrounded){
            player.statesManager.AddState(roarEffect);
        }
    }
    void SwingTail(){
        //Debug.Log("Rock");
        //Maybe animation??
        fallingRocks.GenerateRandomRock();
    }
    void ShotSmoke(){
        Vector3 startPos = new Vector3(head.position.x,PlayerManager.instance.GetPosition().y,head.position.z);
        GameObject smoke = Instantiate(smokePrefab,startPos,Quaternion.identity);
        Smoke smk = smoke.GetComponent<Smoke>();
        smk.SetTarget(PlayerManager.instance.GetPosition());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : MonoBehaviour,IProjectile
{
    [SerializeField]private Transform head;
    [SerializeField]private Transform tail;
    [SerializeField]private float shootInterval;
    [SerializeField]private float roarInterval;
    [SerializeField]private GameObject projectilePrefab;
    [SerializeField]private State roarEffect;
    [SerializeField]private FallingRocks fallingRocks;
    private PlayerManager player;
    private Projectile projectile;
    private float currentTime;
    private int nProjectiles;

    void Start()
    {
        player = PlayerManager.instance;
        nProjectiles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=shootInterval && nProjectiles<3){
            currentTime=0;
            ShotProjectile(head,player.transform.position);
            nProjectiles++;
        }
        if(currentTime>=roarInterval){
            currentTime=0;
            Roar();
            nProjectiles=0;
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
        Debug.Log("ROARRR");
        if(player.isGrounded){
            player.statesManager.AddState(roarEffect);
        }
    }
}

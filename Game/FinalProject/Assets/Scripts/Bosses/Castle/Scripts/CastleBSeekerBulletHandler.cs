using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBSeekerBulletHandler : MonoBehaviour
{
    [SerializeField] private bool activateBasedOnTime;
    [SerializeField] private float timeBtwShot;
    private float curTimeBtwShot;

    private List<StaticBullet> staticBullets;
    private PlayerManager player;


    // Start is called before the first frame update
    void Start()
    {

        player = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            staticBullets = ScenesManagers.GetObjectsOfType<StaticBullet>();
            Vector2 playerPosition = player.GetPosition();
            foreach (var bullet in staticBullets)
            {
                bullet.ActivateBullet(playerPosition);
            }
        }

        if (activateBasedOnTime)
        {
            if (curTimeBtwShot > timeBtwShot)
            {
                Vector2 playerPosition = player.GetPosition();

                staticBullets = ScenesManagers.GetObjectsOfType<StaticBullet>().FindAll(sb => !sb.Projectile.enabled);
                
                if (staticBullets.Count > 0)
                {
                    staticBullets[ Random.Range(0, staticBullets.Count-1) ].ActivateBullet(playerPosition);
                }
                curTimeBtwShot = 0;
            }
            else
            {
                curTimeBtwShot += Time.deltaTime;
            }
        }
    }
}

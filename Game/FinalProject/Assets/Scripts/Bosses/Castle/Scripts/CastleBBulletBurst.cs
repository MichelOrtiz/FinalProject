using UnityEngine;
public class CastleBBulletBurst : MonoBehaviour, IBossFinishedBehaviour
{
    #region TotalTime
    [Header("Total Time")]
    [SerializeField] private float totalTime;
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    private Vector2 shootTarget;
    
    [SerializeField] private float timeBtwShot;

    /*[SerializeField] private float burstTime;
    private float curBurstTime;*/

    [SerializeField] private byte shotsPerBurst;
    private byte curShots;

    [SerializeField] private float timeBtwBurst;
    private float curTimeBtwBurst;
    #endregion

    private PlayerManager player;


    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }


    void Start()
    {
        player = PlayerManager.instance;

        InvokeRepeating("ShootProjectile", timeBtwShot, timeBtwShot);
        Invoke("FinishBehaviour", totalTime);
    }

    void ShootProjectile()
    {
        if (curShots < shotsPerBurst)
        {
            if (curShots == 0)
            {
                shootTarget = (Vector2) player.GetPosition();
            }
            projectileShooter.ShootProjectile(shootTarget);
            curShots++;
        }
    }

    void Update()
    {
        if (curShots >= shotsPerBurst)
        {
            if (curTimeBtwBurst < timeBtwBurst)
            {
                curTimeBtwBurst += Time.deltaTime;
            }
            else
            {
                curTimeBtwBurst = 0;
                curShots = 0;
                CancelInvoke("ShootProjectile");
                InvokeRepeating("ShootProjectile", 0f, timeBtwShot);
            }
        }
    }

    void FinishBehaviour()
    {
        OnFinished(transform.position); 
    }
}

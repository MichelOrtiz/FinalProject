using UnityEngine;
public class CastleBRangeBullets : MonoBehaviour, IBossFinishedBehaviour
{
    #region TotalTime
    [Header("Total Time")]
    [SerializeField] private float totalTime;
    #endregion

    #region ProjectileStuff
    [Header("Projectile Stuff")]
    [SerializeField] private ProjectileShooter projectileShooter;
    private Vector2 shotTarget;
    [SerializeField] private float timeBtwShot;
    [SerializeField] private int shotsPerTime;
    #endregion

    #region TargetRangeStuff
    [Header("Target Range Stuff")]
    
    
    [SerializeField] private float radius;
    
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
        InvokeRepeating("ShootProjectiles", timeBtwShot, timeBtwShot);
        Invoke("FinishBehaviour", totalTime);
    }

    void ShootProjectiles()
    {
        for (int i = 0; i < shotsPerTime; i++)
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Vector2 playerPosition = player.GetPosition();
        
        float x = Random.Range(playerPosition.x - radius, playerPosition.x + radius);
        float y = Random.Range(playerPosition.y - radius, playerPosition.y + radius);

        shotTarget = playerPosition + (Vector2) transform.InverseTransformPoint(new Vector2(x, y)) ;

        projectileShooter.ShootProjectile(shotTarget);
    }

    void FinishBehaviour()
    {
        OnFinished(transform.position); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBSeeker : MonoBehaviour, IBossFinishedBehaviour
{
    private Rigidbody2D rigidbody2d;
    [SerializeField] private float speedMultiplier;
    private float speed;
    private PlayerManager player;

    [SerializeField] private bool followPlayer;
    public bool FollowsPlayer { get => followPlayer; }

    [SerializeField] private Vector2 targetPosition;
   //[SerializeField] private float targetRadius;

    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        speed = Entity.averageSpeed * speedMultiplier;
        player = PlayerManager.instance;
    }
        

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            targetPosition = player.GetPosition();
        }
        else if (rigidbody2d.position == targetPosition)
        {
            OnFinished(transform.position);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        rigidbody2d.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    
}
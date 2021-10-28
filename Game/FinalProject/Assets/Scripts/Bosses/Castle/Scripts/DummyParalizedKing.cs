using UnityEngine;
public class DummyParalizedKing : Entity, IBossFinishedBehaviour
{
    #region TotalTime
    [Header("Total Time")]
    [SerializeField] private float totalTime;
    private float currentTime;
    #endregion
    [SerializeField] private State paralizedState;
    //private BossFight bossFight;

    public event IBossFinishedBehaviour.Finished FinishedHandler;
    public void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }

    new void Start()
    {
        base.Start();
        //bossFight = GetComponent<BossFight>();
        statesManager.hostEntity = this;

        if (paralizedState != null)
        {
            statesManager.AddState(paralizedState);
        }

    }

    new void Update()
    {
        if (currentTime >= totalTime)
        {
            FindObjectOfType<CastleBSeekerBulletHandler>().ActivateBullets();
            OnFinished(transform.position);
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}

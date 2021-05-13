using UnityEngine;
public class CaveBossBehaviour : Entity
{
    public delegate void Finished(Vector2 lastPosition);
    public event Finished FinishedHandler;
    protected virtual void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }

    new protected void Start()
    {
        base.Start();
    }

    new protected void Update()
    {
        base.Update();
    }
}
using UnityEngine;
public interface IBossFinishedBehaviour
{
    delegate void Finished(Vector2 lastPosition);
    event Finished FinishedHandler;
    void OnFinished(Vector2 lastPosition);
}
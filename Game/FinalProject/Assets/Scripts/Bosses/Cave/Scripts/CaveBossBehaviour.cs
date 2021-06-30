using UnityEngine;
public class CaveBossBehaviour : Entity
{

    [SerializeField] protected Color colorWhenHit;
    protected Color defaultColor;
    [SerializeField] protected float timeInColor;
    protected float curTimeInColor;
    protected bool inColor;
    protected SpriteRenderer spriteRenderer;


    public delegate void Finished(Vector2 lastPosition);
    public event Finished FinishedHandler;
    protected virtual void OnFinished(Vector2 lastPosition)
    {
        FinishedHandler?.Invoke(lastPosition);
    }


    new protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        base.Start();
    }

    new protected void Update()
    {
        if (inColor)
        {
            if (curTimeInColor > timeInColor)
            {
                spriteRenderer.color = defaultColor;
                curTimeInColor = 0;
                inColor = false;
            }
            else
            {
                curTimeInColor += Time.deltaTime;
            }
        }
        base.Update();
    }
}
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class EntityDestroyed : MonoBehaviour
{
    public Entity entity;
    [SerializeField] private float lifeTime;
    private float curTime;
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private Material material;
    [SerializeField] private ParticleSystem particles;




    public void Setup(Vector3 position, Quaternion rotation, Vector3 scale, Sprite sprite)
    {
        //this.entity = entity;
        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;

        spriteRenderer.sprite = sprite;
    }


    void Awake()
    {
        particles.Pause();
        particles.GetComponent<Spore>().EmmissionEndedHandler += ParticleSystemStopped;
    }

    void Start()
    {
        //spriteRenderer.material = material;
    }

    void Update()
    {
        if (curTime > lifeTime)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            particles.Play();
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }

    public void ParticleSystemStopped(Spore sender)
    {
        Destroy(gameObject);
    }
}
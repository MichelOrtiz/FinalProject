using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class EntityDestroyed : MonoBehaviour
{
    //public GameObject entity;
    [SerializeField] private float lifeTime;
    private float curTime;
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private Material material;
    [SerializeField] private ParticleSystem particles;


    

    public void Setup(Vector3 position, Quaternion rotation, Vector3 scale, SpriteRenderer spriteRenderer)
    {
        //this.entity = entity;
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;

        this.spriteRenderer.sprite = spriteRenderer.sprite;
        this.spriteRenderer.flipX = spriteRenderer.flipX;
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
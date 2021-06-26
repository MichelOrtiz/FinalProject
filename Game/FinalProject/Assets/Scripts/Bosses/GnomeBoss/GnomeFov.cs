using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GnomeFov : MonoBehaviour
{

    [SerializeField] protected GameObject mesh;
    [SerializeField] protected Material normalMaterial;
    [SerializeField] protected Material afterAttackMaterial;
    [SerializeField] protected Transform groundCheck;

    [SerializeField] protected float damage;
    [SerializeField] protected float baseTimeBeforeAttack;
    [SerializeField] protected float baseTimeAfterAttack;
    [SerializeField] protected float baseTimeUntilChange;
    [SerializeField] protected float speedMultiplier;
    //[SerializeField] private float interval;
    [SerializeField] protected List<Vector2> positions;
    protected float timeUntilChange;
    protected float timeBeforeAttack;
    protected float timeAfterAttack;
    protected bool touchingPlayer;
    public bool justAttacked;
    protected MeshCollision meshCollision;
    protected Vector2 lastPosition;
    //protected float speed;
    int index = 0;
    //private float currentTime;

    protected abstract void Move();

    // Start is called before the first frame update
    protected void Start()
    {
        //speed = speed * Entity.averageSpeed;
        //Mesh mesh = new Mesh();
        
        //GetComponent<MeshFilter>().mesh = mesh;
        mesh.GetComponent<MeshRenderer>().material = normalMaterial;
    }

    // Update is called once per frame
    protected void Update()
    {
        touchingPlayer = mesh.GetComponent<MeshCollision>().touchingPlayer;
        if (justAttacked)
        {
            if (timeAfterAttack > baseTimeAfterAttack)
            {
                timeAfterAttack = 0;
                justAttacked = false;
                mesh.GetComponent<MeshRenderer>().material = normalMaterial;
            }
            else
            {
                timeAfterAttack += Time.deltaTime;
            }
        }
        else if (touchingPlayer)
        {
            if (timeBeforeAttack > baseTimeBeforeAttack)
            {
                Attack();
                mesh.GetComponent<MeshRenderer>().material = afterAttackMaterial;
                lastPosition = transform.position;
                timeBeforeAttack = 0;
            }
            else
            {
                timeBeforeAttack += Time.deltaTime;
            }
            
        }

        //Debug.Log(touchingPlayer? "yesyes":"nono");
    }


    protected void ChangePosition()
    {
        transform.position = positions[index];
        //Debug.Log(index);
        if (index < positions.Count-1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }

    protected bool IsNearEdge()
    {
        return !(Physics2D.Raycast(groundCheck.position, Vector3.down, 1f)).collider;
    }

    protected void Attack()
    {
        PlayerManager.instance.TakeTirement(damage);
        justAttacked = true;
    }


}
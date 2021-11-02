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
    [SerializeField] protected List<Vector2> positions;

    [SerializeField] private EnemyCollisionHandler collisionHandler;
    protected float timeUntilChange;
    protected float timeBeforeAttack;
    protected float timeAfterAttack;
    protected bool touchingPlayer;
    public bool justAttacked;
    protected Vector2 lastPosition;
    int index = 0;

    [SerializeField] private LayerMask whatIsPlatform;

    protected abstract void Move();

    protected void Start()
    {
        mesh.GetComponent<MeshRenderer>().material = normalMaterial;
        collisionHandler.TouchedPlayerHandler += Attack;
    }

    protected void Update()
    {
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
        return !FieldOfView.RayHitObstacle(groundCheck.position, groundCheck.position - groundCheck.up * 1f, whatIsPlatform);
    }

    protected void Attack()
    {
        PlayerManager.instance.TakeTirement(damage);
        justAttacked = true;
        PlayerManager.instance.SetImmune();
    }
}
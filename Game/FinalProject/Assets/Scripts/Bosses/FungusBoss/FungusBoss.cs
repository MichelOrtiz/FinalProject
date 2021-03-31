using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusBoss : NormalType
{
    [SerializeField] private float baseTimeUntilJump;
    [SerializeField] private List<Edge> edges_1;
    [SerializeField] private List<Edge> edges_2;
    [SerializeField] private float jumpYOffset;

    private float timeUntilJump;
    private bool canJump;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (OnEdge())
        {
            if (timeUntilJump > baseTimeUntilJump)
            {
                canJump = true;
                timeUntilJump = 0;
            }
            else
            {
                canJump = false;
                timeUntilJump += Time.deltaTime;
            }
        }
        base.Update();
    }

    new void FixedUpdate()
    {
        if (canJump && isGrounded)
        {
            MoveToNextEdge();
        }

        base.FixedUpdate();
    }
    protected override void MainRoutine()
    {
        /*if (!OnEdge())
        {
            base.MainRoutine();
        }*/
    }

    protected override void ChasePlayer()
    {
        bool facingRight = facingDirection == RIGHT;
        if ( (facingRight && GetPosition().x < player.GetPosition().x) || (!facingRight && GetPosition().x > player.GetPosition().x))
        {
            ChangeFacingDirection();
        }
        float  distanceFromPlayer = GetDistanceFromPlayerFov();
        Vector3 moveDirection = new Vector2(GetPosition().x - player.GetPosition().x, 0f);
        
        //rigidbody2d.position = Vector2.MoveTowards(GetPosition(), moveDirection.normalized * viewDistance, chaseSpeed * Time.deltaTime);

        if (facingRight)
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), moveDirection.normalized, chaseSpeed * Time.deltaTime);
        }
        else
        {
            rigidbody2d.position = Vector2.MoveTowards(GetPosition(), moveDirection.normalized, -chaseSpeed * Time.deltaTime);
        }
    }

    private bool OnEdge()
    {
        return OnSomeEdge1() || OnSomeEdge2();
    }

    private bool OnSomeEdge1()
    {
        //return edges_1.Exists(trans => trans.position.x == GetPosition().x);
        return edges_1.Exists(edge => edge.NearEdge(this));
    }


    private bool OnSomeEdge2()
    {
        //return edges_2.Exists(trans => trans.position.x == GetPosition().x);
        return edges_2.Exists(edge => edge.NearEdge(this));
    }

    private void MoveToNextEdge()
    {
        Edge edgeFrom = new Edge();
        Edge edgeTo = new Edge();
        //float xDistanceToNextEdge;
        //float yDistanceToNextEdge;
        float distanceToNextEdge;
        if (OnSomeEdge1())
        {
            //edgeFrom = edges_1.Find(trans => trans.position.x == GetPosition().x);
            edgeFrom = edges_1.Find(edge => edge.NearEdge(this));
            edgeTo = edges_2[edges_1.IndexOf(edgeFrom)];
        }
        else if (OnSomeEdge2())
        {
            //edgeFrom = edges_2.Find(trans => trans.position.x == GetPosition().x);
            edgeFrom = edges_2.Find(edge => edge.NearEdge(this));
            edgeTo = edges_1[edges_2.IndexOf(edgeFrom)];
        }

        //xDistanceToNextEdge = Mathf.Abs(edgeFrom.transform.position.x - edgeTo.transform.position.x);
        //yDistanceToNextEdge = Mathf.Abs(edgeFrom.transform.position.y - edgeTo.transform.position.y);
        distanceToNextEdge = Vector2.Distance(edgeFrom.transform.position, edgeFrom.transform.position);
        //Push(normalSpeed * 100, jumpForce * 10);
        /*rigidbody2d.AddForce(new Vector2(
            facingDirection == RIGHT? distanceToNextEdge : -distanceToNextEdge,
            edgeTo.transform.position.y + jumpYOffset) * normalSpeed * 100 * Time.deltaTime, ForceMode2D.Impulse);*/
        //rigidbody2d.gravityScale = 0;
        rigidbody2d.position = Vector2.MoveTowards(GetPosition(), edgeTo.transform.position, distanceToNextEdge);
    }
    
}
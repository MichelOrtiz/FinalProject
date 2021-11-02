using UnityEngine;
using System;
public class AbsorbingCristal : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float timeAfterChase;
    private float curTimeAfterChase;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color colorWhenChasing;
    private Color defaultColor;

    private bool curValue;

    [Header("Self Knockback")] 
    [SerializeField] private float selfKnockbackDuration;
    [SerializeField] private float selfKnockBackForce;
    [SerializeField] private float yOffsetKnockback;

    [SerializeReference] private bool justChasedPlayer;
    private bool facingRight;
    [SerializeField] private string darknessName;
    private GameObject darknessObject;

    private DarknessScript darknessScript;
    //[SerializeField] private DarknessScript oscurito;
    



    new void Start()
    {
        base.Start();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;

        darknessObject = GameObject.Find(darknessName);
        darknessScript = darknessObject.GetComponent<DarknessScript>();

        darknessScript.ExitDarkness += darknessScript_Exit;
        //darknessObject = player.Darkness;
    }

    new void Update()
    {
        isJumping = !groundChecker.isGrounded && !isFalling && rigidbody2d.velocity.magnitude != 0;
        facingRight = facingDirection == RIGHT;

        if (!isJumping && !isFalling)
        {
            if ( (facingRight && GetPosition().x < player.GetPosition().x) || (!facingRight && GetPosition().x > player.GetPosition().x))
            {
                ChangeFacingDirection();
            }
        }

        base.Update();
    }

    new void FixedUpdate()
    {
        if (justChasedPlayer)
        {
            if (curTimeAfterChase > timeAfterChase)
            {
                justChasedPlayer = false;
                curTimeAfterChase = 0;
            }
            else
            {
                RunFromPlayer();
                curTimeAfterChase += Time.deltaTime;
            }
        }
        base.FixedUpdate();
    }

    
    void LateUpdate()
    {
        // if ChasePlayer() was just called in update, checks if can not longer see player to update the boolean
        justChasedPlayer = justChasedPlayer && !fieldOfView.canSeePlayer;
    }

    protected override void MainRoutine()
    {
        SetActiveDarkness(true);
        ChangeSpriteColor(defaultColor);

        animationManager.ChangeAnimation("idle");
    }

    protected override void ChasePlayer()
    {
        if (!isJumping)
        {
            RunFromPlayer();
        }
        SetActiveDarkness(false);
        ChangeSpriteColor(colorWhenChasing);
        justChasedPlayer = true;
    }

    protected override void Attack()
    {
        DestroyEntity();
    }

    void RunFromPlayer()
    {
        if (!fieldOfView.inFrontOfObstacle)
        {
            if (isGrounded)
            {
                enemyMovement.Translate(rigidbody2d.transform.right * 2f, chasing: true);
                animationManager.ChangeAnimation("walk");
            }
        }
        else
        {
            ChangeFacingDirection();
            Vector2 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;
            Vector2 knockbackDir = new Vector2(dirToPlayer.x, dirToPlayer.y + yOffsetKnockback );
            Knockback(selfKnockbackDuration, selfKnockBackForce, knockbackDir);
        }

        if (groundChecker.isNearEdge)
        {
            enemyMovement.Jump();
            animationManager.ChangeAnimation("jump");

        }
    }

    void SetActiveDarkness(bool value)
    {

        if (player.isInDark != value)
        {
            curValue = value;
            //darknessObject.GetComponent<DarknessScript>().ObscureLight.SetActive(value);
            darknessScript.SetActiveDarkness(value);
        }
    }

    void ChangeSpriteColor(Color color)
    {
        if (spriteRenderer.color != color)
        {
            spriteRenderer.color = color;
        }
    }

    void darknessScript_Exit()
    {
        //curValue = false;
        Debug.Log("exit called");
        SetActiveDarkness(false);
    }

    void OnDestroy()
    {
        SetActiveDarkness(false);
    }
}
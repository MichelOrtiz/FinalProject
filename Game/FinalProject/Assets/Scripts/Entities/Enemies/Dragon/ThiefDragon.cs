using UnityEngine;
public class ThiefDragon : Enemy
{
    [Header("Self Additions")]
    [SerializeField] private float timeBtwJump;
    private float curTimeBtwJump;
    [SerializeField] private float percentageMoneySteal;
    [SerializeField] private int minMoneyToSteal;
    [SerializeField] private Transform stolenItemIcon;



    //private Inventory inventory;
    new void Start()
    {
        base.Start();
        percentageMoneySteal /= 100;
        InvokeRepeating("Jump", timeBtwJump, timeBtwJump);
    }

    new void FixedUpdate()
    {
        /*if (curTimeBtwJump > timeBtwJump)
        {
            enemyMovement.Jump();
            //ChangeFacingDirection();
            stolenItemIcon.GetComponent<SpriteRenderer>().sprite = null;
            curTimeBtwJump = 0;
        }
        else
        {
            curTimeBtwJump += Time.deltaTime;
        }*/
        base.FixedUpdate();
    }

    void Jump()
    {
        stolenItemIcon.GetComponent<SpriteRenderer>().sprite = null;
        animationManager.ChangeAnimation("jump");
        enemyMovement.Jump();
    }

    protected override void MainRoutine()
    {
        /*if (curTimeBtwJump > timeBtwJump)
        {
            stolenItemIcon.GetComponent<SpriteRenderer>().sprite = null;
            if (groundChecker.isNearEdge || fieldOfView.inFrontOfObstacle)
            {
                ChangeFacingDirection();
                curTimeBtwJump = 0;
            }
            else
            {
                enemyMovement.Jump();
                ChangeFacingDirection();
                curTimeBtwJump = 0;
            }
        }
        else
        {
            curTimeBtwJump += Time.deltaTime;
        }*/
    }

    protected override void Attack()
    {
        //base.Attack();
        Inventory inventory = Inventory.instance;
        int money = Inventory.instance.GetMoney();
        if (money >= minMoneyToSteal)
        {
            int moneyToRemove = Mathf.RoundToInt(money * percentageMoneySteal);
            
            inventory.RemoveMoney(moneyToRemove);
        }
        else if (inventory.items.Count > 0)
        {
            Item itemToRemove = inventory.GetRandomEdibleItem();
            inventory.Remove(itemToRemove);
            stolenItemIcon.GetComponent<SpriteRenderer>().sprite = itemToRemove.icon;
            // poner item en cierta tienda del bazar 7u7
        }
        else
        {
            // player paralized
            player.statesManager.AddState(atackEffect);
        }
    }

    protected override void groundChecker_Grounded(string groundTag)
    {
        animationManager.ChangeAnimation("idle");
        ChangeFacingDirection();
    }
}
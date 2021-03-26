using UnityEngine;
public class ThiefDragon : Dragon
{
    [SerializeField] private float jumpForceX;
    [SerializeField] private float startTimeBtwJump;
    [SerializeField] private float percentageMoneySteal;
    [SerializeField] private int minMoneyToSteal;
    private float timeBtwFloat;
    //private Inventory inventory;
    new void Start()
    {
        base.Start();
        percentageMoneySteal /= 100;
    }

    new void Update()
    {
        base.Update();
    }

    new void FixedUpdate()
    {
        if (timeBtwFloat <= 0)
        {
            Jump(facingDirection == RIGHT? jumpForceX : -jumpForceX);
            timeBtwFloat = startTimeBtwJump;
            ChangeFacingDirection();
        }
        else
        {
            timeBtwFloat -= Time.deltaTime;
        }
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        Inventory inventory = Inventory.instance;
        int money = Inventory.money;
        if (money >= minMoneyToSteal)
        {
            int moneyToRemove = Mathf.RoundToInt(money * percentageMoneySteal);
            
            inventory.RemoveMoney(moneyToRemove);
        }
        else if (inventory.items.Count > 0)
        {
            Item itemToRemove = inventory.GetRandomEdibleItem();
            inventory.Remove(itemToRemove);
            // poner item en cierta tienda del bazar 7u7
        }
        else
        {
            player.statesManager.AddState(atackEffect);
            // player paralized 0.5s
        }
    }
}
using UnityEngine;
[CreateAssetMenu(fileName = "new FungusSporeIncrease", menuName = "States/new FungusSporeIncrease")]
public class FungusSporeIncrease : State
{
    [SerializeField] private bool hasTime;
    [SerializeField] private byte extraSpores;
    private Fungus fungus;
    private FatFungus fatFungus;

    private bool isNormalFungus;


    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (manager.hostEntity is Fungus)
        {
            fungus = manager.hostEntity as Fungus;
            isNormalFungus = true;
        

            fungus.eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_TouchedPlayer;
        }
        else if (manager.hostEntity is FatFungus)
        {
            fatFungus = manager.hostEntity as FatFungus;

            fatFungus.eCollisionHandler.TouchedPlayerHandler += eCollisionHandler_TouchedPlayer;
        }


    }
    public override void Affect()
    {
        if (hasTime)
        {
            if (currentTime > duration)
            {
                currentTime = 0;
                StopAffect();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    void eCollisionHandler_TouchedPlayer()
    {
        if (isNormalFungus)
        {
            Debug.Log("sadas");
            for (int i = 0; i < extraSpores; i++)
            {
                fungus.ForceAttack();
            }
        }
        else
        {
            for (int i = 0; i < extraSpores; i++)
            {
                fatFungus.ForceAttack();
            }
        }
    }
}

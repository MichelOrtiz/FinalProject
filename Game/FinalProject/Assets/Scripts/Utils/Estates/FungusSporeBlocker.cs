using UnityEngine;

[CreateAssetMenu(fileName = "new FungusSporeBlocker", menuName = "States/new FungusSporeBlocker")]
public class FungusSporeBlocker : State
{
    private Fungus fungus;
    private FatFungus fatFungus;

    private bool isNormalFungus;
    private byte defMaxSpores;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        if (manager.hostEntity is Fungus)
        {
            fungus = manager.hostEntity as Fungus;
            defMaxSpores = fungus.maxSpores;
            isNormalFungus = true;
        
            fungus.maxSpores = 0;
        }
        else if (manager.hostEntity is FatFungus)
        {
            fatFungus = manager.hostEntity as FatFungus;
            defMaxSpores = fatFungus.maxSpores;

            fatFungus.maxSpores = 0;
        }
    }
    
    public override void Affect()
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

    public override void StopAffect()
    {
        if (isNormalFungus)
        {
            fungus.maxSpores = defMaxSpores;
        }
        else
        {
            fatFungus.maxSpores = defMaxSpores;
        }
        base.StopAffect();
    }
}
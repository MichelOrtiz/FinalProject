using UnityEngine;
using System.Collections.Generic;
public class FatFungus : FatType
{
    [SerializeField] private byte sporesForState;
    private byte currentSpores;
    [SerializeField] private List<State> states;

    protected override void Attack()
    {
        base.Attack();
        currentSpores++;

        if (currentSpores >= sporesForState)
        {
            try
            {
                player.statesManager.AddState( RandomGenerator.RandomElement<State>(states) );
            }
            catch (System.NullReferenceException)
            {
                    Debug.LogError("No states to add from " + gameObject);
            }
            currentSpores = 0;
        }
    }
}
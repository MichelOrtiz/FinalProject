using UnityEngine;
using System.Collections.Generic;
public class FatFungus : FatType
{
    [Header("Self Additions")]
    public byte maxSpores;
    public byte currentSpores;
    [SerializeField] private List<State> states;
    [SerializeField] private FollowerObject followerObject;

    protected override void Attack()
    {
        base.Attack();
        var manager = player.GetComponentInChildren<FollowerObjectManager>();
        manager.AddFollower(followerObject);

        currentSpores = (byte)ScenesManagers.GetObjectsOfType<FollowerObject>()?.FindAll(f => f.type == FollowerObject.FollowerType.Spore && f.target == player.gameObject)?.Count;

        if (currentSpores >= maxSpores)
        {
            try
            {
                player.statesManager.AddState( RandomGenerator.RandomElement<State>(states) );
            }
            catch (System.Exception)
            {
                Debug.Log("No states to add from " + gameObject);
            }
            manager.DestroyAllFollowers();
            currentSpores = 0;
        }
    }
}
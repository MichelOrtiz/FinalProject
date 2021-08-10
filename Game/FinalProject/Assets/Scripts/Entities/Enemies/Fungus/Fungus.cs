using UnityEngine;
using System.Collections.Generic;

public class Fungus : NormalType
{
    [Header("Self Additions")]
    [SerializeField] private byte maxSpores;
    private byte currentSpores;
    [SerializeField] private List<State> states;
    [SerializeField] private FollowerObject followerObject;

    new void Start()
    {
        base.Start();
        //followerObject.OnAwake += followerObject_OnAwake;
    }


    protected override void Attack()
    {
        base.Attack();

        //Instantiate(followerObject, GetPosition(), followerObject.transform.rotation).GetComponent<FollowerObject>().Target = player.gameObject;
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

    /*void followerObject_OnAwake()
    {
        currentSpores++;
        if ( curr )
    }*/
}
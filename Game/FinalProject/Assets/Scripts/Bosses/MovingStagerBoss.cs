using UnityEngine;
using System.Collections.Generic;
public abstract class MovingStagerBossBehaviour : MonoBehaviour
{
    protected PlayerManager player;

    /// <summary>
    /// Objects with behaviour defined
    /// </summary>
    [SerializeField] protected List<GameObject> children;
    [SerializeField] protected GameObject child;
    private GameObject nextChild;

    [SerializeField] private float timeBtwAttacks;
    private float currentTimeBtwAttack;
    private bool changingAttack;


    protected virtual void Start()
    {
        player = PlayerManager.instance;
        nextChild = children[0];
        ChangeChild(nextChild);
    }

    protected virtual void UpdateChildrenPosition()
    {
        
    }

    protected virtual void ChangeChild(GameObject gameObject)
    {
        //child.SetActive(false);
        //child = gameObject;
        //child.SetActive(true);
        if (child != null)
        {
            child.GetComponent<UBBehaviour>().SetActive(false);
        }

        child = gameObject;
        child.GetComponent<UBBehaviour>().SetActive(true);


        gameObject.transform.localPosition = transform.position;
    }

    protected void FinishChildAttack()
    {
        child.GetComponent<UBBehaviour>().FinishAttack();
    }

}
using UnityEngine;
using System.Collections.Generic;
public class MimicCrystal : MonoBehaviour
{
    [Header("Berry Checking")]
    [SerializeField] private Transform berryChecker;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private CollisionHandler detectionCollider;

    
    [Header("Enemy \"Mimic\"")]
    [Header("General")]
    [SerializeField] private List<ItemEntitySpawn> itemEntitySpawns;
    [SerializeField] private Color mimicColor;


    [Header("Before Spawn")]
    [SerializeField] private float stopTime;
    private float curStopTime;
    private ItemEntitySpawn nextEnemy;


    [Header("After Spawn")]
    [SerializeField] private Vector2 knockbackDir;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackForce;


    [Header("During Spawn")]
    [SerializeField] private Transform enemyHolder;
    [SerializeReference] private Enemy currentEnemy;


    void Awake()
    {
        detectionCollider.EnterTouchingContactHandler += collisionHandler_EnterTouchingContact;
    }

    void Start()
    {
        //berryChecker.transform.SetParent(enemyHolder);
        // Sets a random initial enemy
        ItemEntitySpawn itemEntitySpawn = RandomGenerator.RandomElement<ItemEntitySpawn>(itemEntitySpawns);
        SetCurrentEnemy(itemEntitySpawn);
    }

    void Update()
    {
        if (nextEnemy != null)
        {
            if (curStopTime > stopTime)
            {
                SetCurrentEnemy(nextEnemy);
                nextEnemy = null;
                //currentEnemy.Knockback(knockbackTime, knockbackForce, knockbackDir);
                curStopTime = 0;
            }
            else
            {
                // Stop enemy behaviour
                if (currentEnemy.enabled)
                {
                    currentEnemy.EnemyMovement.StopMovement();
                    currentEnemy.enabled = false;
                }
                curStopTime += Time.deltaTime;
            }
        }
    }

    void SetCurrentEnemy(ItemEntitySpawn itemEntitySpawn)
    {
        // saves current values to restore them after the enemy change
        Quaternion oldRotation = transform.rotation;
        Vector2 oldPosition = enemyHolder.localPosition;
        if (currentEnemy != null)
        {
            Destroy(currentEnemy.gameObject);
            oldRotation = currentEnemy.transform.rotation;
        }
        currentEnemy = Instantiate((Enemy)itemEntitySpawn.entity, itemEntitySpawn.spawnPos.position, oldRotation);
        enemyHolder.SetParent(currentEnemy.transform);
        //currentEnemy.gameObject.transform.SetParent(enemyHolder);
        enemyHolder.localPosition = oldPosition;
        currentEnemy.GetComponentInChildren<SpriteRenderer>().color = mimicColor;
    }


    void collisionHandler_EnterTouchingContact(GameObject contact)
    {
        try
        {
            // Finds the list element with the same item hit by the collider
            ItemEntitySpawn itemEntitySpawn = itemEntitySpawns.Find(ies => ies.item == contact.GetComponent<Inter>().item);
            
            // Changes the next enemy if it's not the current one
            if (currentEnemy.enemyName != itemEntitySpawn.entity.GetComponent<Enemy>().enemyName)
            {
                nextEnemy = itemEntitySpawn;
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("couldn't find the right enemy bro");
        }
    }
    
}
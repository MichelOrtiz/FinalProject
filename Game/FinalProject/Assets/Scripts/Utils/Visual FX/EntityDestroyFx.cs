using UnityEngine;
using System.Collections.Generic;
public class EntityDestroyFx : MonoBehaviour
{
    [SerializeField] private EntityDestroyed entityDestroyed;
    public static EntityDestroyFx Instance { get;  private set; }

    void Awake()
    {
        Instance = this;
        DestroyAllEntitiesDestroyed();
    }
    public void StartDestroyFx(Entity entity)
    {
        var newEntity = entity;
        /*if (newEntity == null)
        {
            newEntity = Instantiate(entity, )
        }*/
        entityDestroyed.Setup(newEntity.transform.position, newEntity.transform.rotation, newEntity.transform.localScale, newEntity.GetComponentInChildren<SpriteRenderer>());
        Instantiate(entityDestroyed, newEntity.GetPosition(), newEntity.transform.rotation);
    }

    public void StartDestroyFx(GameObject gmObj)
    {
        var newEntity = gmObj;
        /*if (newEntity == null)
        {
            newEntity = Instantiate(entity, )
        }*/
        entityDestroyed.Setup(newEntity.transform.position, newEntity.transform.rotation, newEntity.transform.localScale, newEntity.GetComponentInChildren<SpriteRenderer>());
        Instantiate(entityDestroyed, newEntity.transform.position, newEntity.transform.rotation);
    }

    public void DestroyAllEntitiesDestroyed()
    {
        int index = 0;
        List<EntityDestroyed> entitiesDestroyed = ScenesManagers.GetObjectsOfType<EntityDestroyed>();
        if (entitiesDestroyed != null)
        {
            while(entitiesDestroyed.Count > 0)
            {
                Destroy(entitiesDestroyed[index].gameObject);
                entitiesDestroyed.Remove(entitiesDestroyed[index]);
            }
        }

    }

    void OnDestroy()
    {
        DestroyAllEntitiesDestroyed();
    }
}
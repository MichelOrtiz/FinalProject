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

    public void DestroyAllEntitiesDestroyed()
    {
        int index = 0;
        List<EntityDestroyed> entityDestroyeds = ScenesManagers.GetObjectsOfType<EntityDestroyed>();
        if (entityDestroyeds != null)
        {
            while(entityDestroyeds.Count > 0)
            {
                Destroy(entityDestroyeds[index].gameObject);
                entityDestroyeds.Remove(entityDestroyeds[index]);
            }
        }

    }

    void OnDestroy()
    {
        DestroyAllEntitiesDestroyed();
    }
}
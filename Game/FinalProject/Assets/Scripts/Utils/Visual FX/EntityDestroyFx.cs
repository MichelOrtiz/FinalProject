using UnityEngine;
using System.Collections.Generic;
public class EntityDestroyFx : MonoBehaviour
{
    [SerializeField] private EntityDestroyed entityDestroyed;
    public static EntityDestroyFx Instance { get;  private set; }
    byte count;

    void Awake()
    {
        Instance = this;
        DestroyAllEntitiesDestroyed();
    }
    public void StartDestroyFx(Entity entity)
    {
        //entity.gameObject.SetActive(false);
        /*if (newEntity == null)
        {
            newEntity = Instantiate(entity, )
        }*/
        //entityDestroyed = new EntityDestroyed(newEntity.transform.position, newEntity.transform.rotation, newEntity.transform.localScale, newEntity.GetComponentInChildren<SpriteRenderer>());
        var entDes = Instantiate(entityDestroyed, entity.transform.position, entity.transform.rotation);
        entDes.Setup(entity.transform.position, entity.transform.rotation, entity.transform.localScale, entity.GetComponentInChildren<SpriteRenderer>());
    }

    public void StartDestroyFx(GameObject gmObj)
    {
        //gmObj.SetActive(false);
        /*if (newEntity == null)
        {
            newEntity = Instantiate(entity, )
        }*/
        //entityDestroyed = new EntityDestroyed(newEntity.transform.position, newEntity.transform.rotation, newEntity.transform.localScale, newEntity.GetComponentInChildren<SpriteRenderer>());
        var endDes = Instantiate(entityDestroyed, gmObj.transform.position, gmObj.transform.rotation);
        endDes.Setup(gmObj.transform.position, gmObj.transform.rotation, gmObj.transform.localScale, gmObj.GetComponentInChildren<SpriteRenderer>());
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
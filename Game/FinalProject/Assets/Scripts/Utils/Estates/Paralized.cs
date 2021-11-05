using UnityEngine;

[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Paralisis")]
public class Paralized : State
{
    bool isPlayer;
    PlayerManager player;
    Entity entity;

    //bool entityDisabled;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        //bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        //Debug.Log("player: " + isPlayer);
        /*if(manager.hostEntity is PlayerManager)
        {
            player = manager.hostEntity as PlayerManager;
            player.inputs.enabled=false;
            player.abilityManager.enabled = false;
            Debug.Log("kdspajd");
        }else{
            manager.hostEntity.enabled=false;
        }*/
        entity = manager.hostEntity;
        isPlayer = entity is PlayerManager;
        if (isPlayer)
        {
            player = entity as PlayerManager;
        }
    }
    public override void Affect()
    {
        SetActiveEntity(false);

        Rigidbody2D rb = manager.hostEntity.GetComponent<Rigidbody2D>(); 
        rb.velocity = new Vector2(0f,-rb.gravityScale);
        currentTime += Time.deltaTime;
        //Debug.Log(manager.gameObject + ": " + currentTime);
        if(currentTime >= duration){
            StopAffect();
        }
    }

    void SetActiveEntity(bool value)
    {
        if (isPlayer)
        {
            if (player.inputs.enabled != value) player.inputs.enabled = value;
            if (player.abilityManager.abiltySystem.activeInHierarchy != value) player.abilityManager.SetActive(value);
        }
        else
        {
            if (entity.enabled != value) entity.enabled = value;
        }
    }

    public override void StopAffect()
    {
        base.StopAffect();
        SetActiveEntity(true);
    }
}
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
        DisableEntity();

        Rigidbody2D rb = manager.hostEntity.GetComponent<Rigidbody2D>(); 
        rb.velocity = new Vector2(0f,-rb.gravityScale);
        currentTime += Time.deltaTime;
        //Debug.Log(manager.gameObject + ": " + currentTime);
        if(currentTime >= duration){
            StopAffect();
        }
    }

    void DisableEntity()
    {
        if (isPlayer)
        {
            if (player.inputs.enabled) player.inputs.enabled = false;
            if (player.abilityManager.gameObject.activeInHierarchy) player.abilityManager.gameObject.SetActive(false);
        }
        else
        {
            if (entity.enabled) entity.enabled = false;
        }
    }

    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            manager.hostEntity.GetComponent<PlayerInputs>().enabled=true;
            manager.hostEntity.GetComponent<PlayerManager>().abilityManager.SetActive(true);
        }else{
            manager.hostEntity.enabled=true;
        }
        if (manager.hostEntity is PlayerManager)
        {
            var player = manager.hostEntity as PlayerManager;
            player.SetImmune();
        }
    }
}
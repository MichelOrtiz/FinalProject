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
        entity = manager.hostEntity;
        isPlayer = entity is PlayerManager;
        if (isPlayer)
        {
            player = entity as PlayerManager;
            if (player.currentStamina < 1f)
            {
                StopAffect();
            }
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
            if (player.inputs.enabled != value) player.SetEnabledPlayer(value);
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
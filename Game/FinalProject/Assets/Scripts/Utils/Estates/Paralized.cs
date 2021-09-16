using UnityEngine;

[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Paralisis")]
public class Paralized : State
{//
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            manager.hostEntity.GetComponent<PlayerInputs>().enabled=false;
            manager.hostEntity.GetComponent<PlayerManager>().abilityManager.SetActive(false);
        }else{
            manager.hostEntity.enabled=false;
        }
    }
    public override void Affect()
    {
        Rigidbody2D rb = manager.hostEntity.GetComponent<Rigidbody2D>(); 
        rb.velocity = new Vector2(0f,-rb.gravityScale);
        currentTime += Time.deltaTime;
        //Debug.Log(manager.gameObject + ": " + currentTime);
        if(currentTime >= duration){
            StopAffect();
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
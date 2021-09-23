using UnityEngine;
[CreateAssetMenu(fileName="New State", menuName = "States/Items/Flan")]
public class FlanState : State
{
    [SerializeField] private float jumpMultiplier;
    float defaultJumpForce;
    bool jumped = false;
    PlayerManager player = null;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            player = manager.hostEntity.GetComponent<PlayerManager>();
            defaultJumpForce = player.GetJumpForce();
            player.SetJumpForce(player.GetJumpForce() * jumpMultiplier);
            jumped = false;

        }
        else if (manager.hostEntity.GetComponent<Enemy>() != null)
        {
            StopAffect();
        }
    }
    public override void Affect()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= duration){
            StopAffect();
        }
        //Solo puede usarse una vez SAD
        if(player.isJumping){
            jumped = true;
        }
        if(jumped){
            if(!player.isJumping){
                StopAffect();
            }
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            player.SetJumpForce(defaultJumpForce);
        }
    }
}

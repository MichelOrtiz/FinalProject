using UnityEngine;
[CreateAssetMenu(fileName="New State", menuName = "States/Items/Naranja")]
public class NaranjaState : State
{
    [SerializeField]
    private float inputLag;
    PlayerManager player;
    public override void Affect()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= duration){
            StopAffect();
        }
    }
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            player = manager.hostEntity.GetComponent<PlayerManager>();
            player.inputs.intputLag = inputLag;
        }
        else if (manager.hostEntity.GetComponent<Enemy>() != null)
        {
            StopAffect();
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            PlayerManager.instance.inputs.intputLag = PlayerInputs.defaultInputLag;
        }
        
    }
}

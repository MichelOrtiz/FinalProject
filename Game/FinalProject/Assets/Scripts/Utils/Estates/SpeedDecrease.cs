using UnityEngine;

[CreateAssetMenu(fileName="New SpeedDecrease", menuName = "States/new Speed Decrease")]
public class SpeedDecrease : State
{
    [SerializeField] private float decreaseMultiplier;
    private float defaultWalkingSpeed;
    private float defaultRunningSpeed;
    PlayerManager player;
    Enemy enemy;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer)
        {
            player = manager.hostEntity.GetComponent<PlayerManager>();
            defaultWalkingSpeed = player.defaultwalkingSpeed;

            player.walkingSpeed *= decreaseMultiplier;
        }
        else if (manager.hostEntity.GetComponent<Enemy>() != null)
        {
            enemy = manager.hostEntity.GetComponent<Enemy>();
            defaultWalkingSpeed = enemy.normalSpeed;
            defaultRunningSpeed = enemy.chaseSpeed;

            enemy.normalSpeed *= decreaseMultiplier;
            enemy.chaseSpeed *= decreaseMultiplier;
        }
    }
    public override void Affect()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= duration){
            StopAffect();
        }
    }
    public override void StopAffect()
    {
        base.StopAffect();
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            player.walkingSpeed = defaultWalkingSpeed;
        }else{
            enemy.normalSpeed = defaultWalkingSpeed;
            enemy.chaseSpeed = defaultRunningSpeed;
        }
    }
}
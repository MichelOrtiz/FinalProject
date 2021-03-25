using UnityEngine;

[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Paralisis")]
public class Paralized : State
{
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        bool isPlayer = manager.hostEntity.GetComponent<PlayerManager>() != null;
        if(isPlayer){
            manager.hostEntity.GetComponent<PlayerInputs>().enabled=false;
        }else{
            manager.hostEntity.enabled=false;
        }
    }
    public override void Affect()
    {
        //Debug.Log(currentTime);
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
            manager.hostEntity.GetComponent<PlayerInputs>().enabled=true;
        }else{
            manager.hostEntity.enabled=true;
        }
    }
}
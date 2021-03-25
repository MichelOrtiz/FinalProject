using UnityEngine;

[CreateAssetMenu(fileName="New Paralisis", menuName = "States/new Paralisis")]
public class Paralized : State
{
    Vector3 paralizatedPos;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        paralizatedPos = manager.hostEntity.GetPosition();
    }
    public override void Affect()
    {
        //Debug.Log(currentTime);
        currentTime += Time.deltaTime;
        if(currentTime >= duration){
            StopAffect();
        }
        manager.hostEntity.gameObject.transform.position = paralizatedPos;
    }
}
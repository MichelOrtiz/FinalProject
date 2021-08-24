using UnityEngine;
[CreateAssetMenu(fileName = "new LeaveAndReturn", menuName = "States/new LeaveAndReturn")]
public class LeaveAndReturn : State
{
    [SerializeField] private State stateWhenReturn;
    private GameObject newEntity;
    private ProbabilitySpawner probabilitySpawner;

    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        StopAffect();
    }

    public override void Affect()
    {
        //StopAffect();
    }

    public override void StopAffect()
    {
        var probabilitySpawner = ScenesManagers.FindObjectOfType<ProbabilitySpawner>();
        var spawnObject = probabilitySpawner.SpawnedObjects.Find(sp => sp.gameObject == manager.hostEntity.gameObject);
        //var spawn = probabilitySpawner.ProbabilitySpawns.Find(p => p == probabilitySpawn);
        if (spawnObject != null)
        {
            Enemy enemy = Instantiate(spawnObject.gameObject, spawnObject.spawnedPos, spawnObject.gameObject.transform.rotation).GetComponentInChildren<Enemy>();
            enemy.statesManager.AddState(stateWhenReturn);
        }
        manager.hostEntity.DestroyEntity();
    }
}
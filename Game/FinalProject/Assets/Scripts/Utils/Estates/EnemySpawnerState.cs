using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName="New EnemySpawnerState", menuName = "States/new EnemySpawnerState")]
public class EnemySpawnerState : State
{
    [SerializeField] private byte minQuantity;
    [SerializeField] private byte maxQuantity;
    private List<Transform> positions;
    private Enemy enemy;
    private Enemy hostEnemy;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);
        try
        {
            hostEnemy = (Enemy) manager.hostEntity;
            var probabilitySpawner = ScenesManagers.FindObjectOfType<ProbabilitySpawner>();
            var probabilitySpawn = probabilitySpawner.GetProbabilitySpawnFrom(hostEnemy.gameObject);
            //var spawn = probabilitySpawner.ProbabilitySpawns.Find(p => p == probabilitySpawn);
            var newSpawn = new ProbabilitySpawn(hostEnemy.gameObject, probabilitySpawn.positions, 100, minQuantity, maxQuantity);
            //Debug.Log(nwSpwns.gameObject);
            probabilitySpawner.Spawn(newSpawn);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error from EnemySpawnerState: " + e.Message);
            StopAffect();
        }
    }
    public override void Affect()
    {
        if (currentTime > duration)
        {
            StopAffect();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
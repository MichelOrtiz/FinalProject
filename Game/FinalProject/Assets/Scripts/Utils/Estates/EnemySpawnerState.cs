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
            var probabilitySpawns = probabilitySpawner.ProbabilitySpawns;
            var spawns = probabilitySpawns.FindAll(p => p.gameObject.GetComponent<Enemy>());
            
            List<Enemy> enemies = new List<Enemy>();
            spawns.ForEach(s => enemies.Add(s.gameObject.GetComponent<Enemy>()));
            enemy = enemies.Find(e => e.enemyName == hostEnemy.enemyName);

            positions = spawns.Find(s => s.gameObject == enemy.gameObject).positions;

            var newSpawns = new ProbabilitySpawn(enemy.gameObject, positions, 100, minQuantity, maxQuantity);

            probabilitySpawner.Spawn(newSpawns);
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
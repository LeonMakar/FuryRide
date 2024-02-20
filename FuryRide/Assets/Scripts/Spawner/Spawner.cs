using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> SpawnPositions;
    protected EnemySpawnChanceData EnemySpawnData;
    protected BonusSpawnChanceData BonusSpawnData;


    protected bool GameIsActive;


    public void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    public void SetGameActivity(bool boolian)
    {
        GameIsActive = boolian;
    }

    protected abstract IEnumerator Spawning();

    protected Transform RandomizeSpawnPosition()
    {
        var x = UnityEngine.Random.Range(0, SpawnPositions.Count);
        return SpawnPositions[x];
    }
}

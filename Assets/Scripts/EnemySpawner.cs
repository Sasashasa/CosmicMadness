using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyPack
    {
        public GameObject _enemy;
        public int _repetitionsAmount;
    }
    
    [System.Serializable]
    private struct LevelPack
    {
        public float SpawnCooldown;
        public EnemyPack[] _enemiesPacks;
    }
    
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private LevelPack[] _levelPacks;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        LevelPack levelPack = _levelPacks[Stats.CurLevel];
        
        foreach (EnemyPack enemyPack in levelPack._enemiesPacks)
        {
            for (int i = 0; i < enemyPack._repetitionsAmount; i++)
            {
                Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
                Instantiate(enemyPack._enemy, spawnPoint, Quaternion.identity);
                yield return new WaitForSeconds(levelPack.SpawnCooldown);
            }
        }
    }
}
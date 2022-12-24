using System.Collections;
using UnityEngine;

public class LevelSpawn : MonoBehaviour
{
    [Header("[SpawnPoints]")]
    [SerializeField] private Transform _spawnPoint;
    [Header("[LevelLogic]")]
    [SerializeField] private LevelController _levelController;

    private int _delaySpawn = 8;
    private IEnumerator _spawnEnemy;

    public void StartSpawn(Levels levels)
    {
        _spawnEnemy = SpawnEnemy(levels.EnemyPrefab, levels.CountEnemy);
        StartCoroutine(_spawnEnemy);
    }

    public void FindEnemy()
    {
        if (_spawnEnemy != null)
        {
            StopCoroutine(_spawnEnemy);
        }

        var enemy = FindObjectsOfType<Enemy>();
        DestroyObjects(enemy);
    }

    private void DestroyObjects(Enemy[] enemy)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i].gameObject);
        }
    }

    private IEnumerator SpawnEnemy(Enemy enemy, int countEnemy)
    {
        while (countEnemy > 0)
        {
            yield return new WaitForSeconds(_delaySpawn);

            CreateEnemy(enemy);
            countEnemy--;
        }

        if (_spawnEnemy != null)
        {
            StopCoroutine(_spawnEnemy);
        }
    }

    private void CreateEnemy(Enemy template)
    {
        Enemy enemy = Instantiate(template, new Vector3(_spawnPoint.localPosition.x, _spawnPoint.localPosition.y, _spawnPoint.localPosition.z), new Quaternion(0, 180, 0, 0));
        enemy.Dying += _levelController.OnEnemyDie;
    }
}
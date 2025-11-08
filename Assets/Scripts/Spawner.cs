using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[AddComponentMenu("Game/Spawners/Spawner")]
public class Spawner : MonoBehaviour
{
    [Header("Settings Map: Points")]
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    [Header("Spawn Settings")]
    [SerializeField] private int _maxPoolSize = 100;
    [SerializeField] private float _spawnDelay = 2f;

    [Header("Resources")]
    [SerializeField] private Enemy _enemyPrefab;

    private int _prewarmEnemyCount = 30;
    private bool _canSpawn = true;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _spawnWait;
    private CustomPool<Enemy> _pool;

    private HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();

    private void Start()
    {
        _pool = new CustomPool<Enemy>(_enemyPrefab, _prewarmEnemyCount, _maxPoolSize);
        _spawnWait = new WaitForSeconds(_spawnDelay);
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        _canSpawn = false;

        if(_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        foreach(var enemy in _activeEnemies)
        {
            if (enemy != null)
                enemy.ExitedFromArea -= OnEnemyExitedArea;
        }

        _activeEnemies.Clear();
    }

    private IEnumerator SpawnRoutine()
    {
        yield return null;

        while (_canSpawn)
        {
            Spawned();

            yield return _spawnWait;
        }
    }

    private void Spawned()
    {
        Enemy enemy = _pool.Get();
        if (enemy == null) return;

        ConfigureEnemy(enemy);

        enemy.ExitedFromArea += OnEnemyExitedArea;
        _activeEnemies.Add(enemy);
    }

    private void ConfigureEnemy(Enemy enemy)
    {
        SpawnPoint spawnPoint = GetRandomPointPosition();
        enemy.transform.position = spawnPoint.GetPoint();
        enemy.transform.rotation = spawnPoint.GetRotation();
        enemy.Move(spawnPoint.GetDirection());
    }

    private void OnEnemyExitedArea(Enemy enemy)
    {
        enemy.ExitedFromArea -= OnEnemyExitedArea;
        _activeEnemies.Remove(enemy);
                
        enemy.Reset();
        _pool.Release(enemy);
    }

    private SpawnPoint GetRandomPointPosition()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
            throw new System.ArgumentException("spawnPoints");

        int minRandomIndex = 0;

        int randomIndex = Random.Range(minRandomIndex, _spawnPoints.Count);

        return _spawnPoints[randomIndex];
    } 
}
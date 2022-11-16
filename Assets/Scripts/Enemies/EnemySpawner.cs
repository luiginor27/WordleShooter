using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 spawnDelay;
    public float maxEnemies;

    public Transform player;
    public float minDistancePlayer = 5f;

    public List<Transform> limits;
    public List<GameObject> pooling;

    private float _minX = float.MaxValue;
    private float _maxX = float.MinValue;
    private float _minZ = float.MaxValue;
    private float _maxZ = float.MinValue;

    private float _spawnTimer;
    private float _enemyCount;

    void Start()
    {
        enabled = false;

        // Establish the position limits for the spawn
        foreach (Transform limit in limits)
        {
            if (limit.position.x < _minX) _minX = limit.position.x;
            if (limit.position.x > _maxX) _maxX = limit.position.x;

            if (limit.position.z < _minZ) _minZ = limit.position.z;
            if (limit.position.z > _maxZ) _maxZ = limit.position.z;
        }
    }

    void Update()
    {
        if (_spawnTimer > 0)
        {
            _spawnTimer -= Time.deltaTime;
        }
        else if (_enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
        else if (EnemiesAlive() == 0)
        {
            GameManager.Instance.EndLevel();
            enabled = false;
        }
    }

    private void OnEnable()
    {
        PlayPauseEnemies(true);
    }

    private void OnDisable()
    {
        PlayPauseEnemies(false);
    }

    private void PlayPauseEnemies(bool play)
    {
        foreach (GameObject enemy in pooling)
        {
            if (enemy.activeSelf) enemy.GetComponent<EnemyController>().enabled = play;
        }
    }

    public void ResetState()
    {
        HideEnemies();

        enabled = false;
        _enemyCount = 0;
        _spawnTimer = 0;
    }

    private void HideEnemies()
    {
        foreach (GameObject enemy in pooling)
        {
            enemy.SetActive(false);
        }
    }

    private void SpawnEnemy()
    {
        _spawnTimer = Random.Range(spawnDelay.x, spawnDelay.y);
        _enemyCount++;

        // Get the first game object from the pooling that's not active
        GameObject enemyGameObject = GetGameObjectFromPooling();

        if (enemyGameObject != null)
        {
            // Get the spawn position given the limits and the minimum distance
            Vector3 enemyPosition = GetEnemyPosition();

            enemyGameObject.transform.position = enemyPosition;
            enemyGameObject.transform.rotation = Quaternion.identity;
            enemyGameObject.SetActive(true);
        }
    }

    private GameObject GetGameObjectFromPooling()
    {
        foreach (GameObject enemy in pooling)
        {
            if (!enemy.activeSelf) return enemy;
        }
        return null;
    }

    private Vector3 GetEnemyPosition()
    {
        Vector3 enemyPosition = Vector3.zero;

        while (enemyPosition.Equals(Vector3.zero) || Vector3.Distance(player.position, enemyPosition) < minDistancePlayer)
        {
            float xSpawn = Random.Range(_minX, _maxX);
            float zSpawn = Random.Range(_minZ, _maxZ);
            enemyPosition = new Vector3(xSpawn, 2f, zSpawn);
        }

        return enemyPosition;
    }

    private int EnemiesAlive()
    {
        int count = 0;
        foreach (GameObject enemy in pooling)
        {
            if (enemy.activeSelf) count++;
        }
        return count;
    }

   
}

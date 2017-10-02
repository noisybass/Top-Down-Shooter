using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private Transform _enemiesTarget;
    private Pool<Enemy> _enemyPool;

    [SerializeField]
    private float _spawnSeconds = 2.0f;
    [SerializeField]
    private float _spawnDistance = 10.0f;
    private Vector2 _center;

    

    void Awake () {
        _center = transform.position;
        _enemyPool = new Pool<Enemy>(10, _enemyPrefab, gameObject);
	}

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(_spawnSeconds);
        }
    }

    void CreateEnemy()
    {
        Vector3 spawnPos = Vector3.zero;
        spawnPos.x = _spawnDistance * Random.Range(-1.0f, 1.0f) + _center.x;
        spawnPos.y = _spawnDistance * Random.Range(-1.0f, 1.0f) + _center.y;

        Enemy enemy = _enemyPool.CreateObject();
        enemy.Init(spawnPos, _enemiesTarget);
    }

    public void DestroyEnemy(Enemy enemy)
    {
        _enemyPool.DestroyObject(enemy);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _spawnDistance);
    }
}

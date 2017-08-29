using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private Transform _enemiesTarget;
    [SerializeField]
    private float _spawnSeconds = 2.0f;
    [SerializeField]
    private float _spawnDistance = 10.0f;
    private Vector2 _center;

    void Awake () {
        _center = transform.position;
	}

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.x = _spawnDistance * Random.Range(-1.0f, 1.0f) + _center.x;
            spawnPos.y = _spawnDistance * Random.Range(-1.0f, 1.0f) + _center.y;
            Enemy enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            enemy.Target = _enemiesTarget;
            yield return new WaitForSeconds(_spawnSeconds);
        }
    }
}

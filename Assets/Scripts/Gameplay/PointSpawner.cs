using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour {

    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private Transform _enemiesTarget;
    private Pool<Enemy> _enemyPool;

    [SerializeField]
    private float _spawnSeconds = 2.0f;

    void Awake()
    {
        _enemyPool = new Pool<Enemy>(10, _enemyPrefab, gameObject);
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(_spawnSeconds);
        }
    }

    void CreateEnemy()
    {
        Enemy enemy = _enemyPool.CreateObject();
        enemy.Init(transform.position, _enemiesTarget);
        EffectsManager.Instance.CreateEffect(EffectsManager.EffectType.MONSTER_SPAWN, transform.position, false);
    }

    public void DestroyEnemy(Enemy enemy)
    {
        _enemyPool.DestroyObject(enemy);
    }
}

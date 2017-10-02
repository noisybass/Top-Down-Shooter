using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float _upp;
    [SerializeField]
    private float _enemySpeed = 100;

    [SerializeField]
    private float _enemyLife = 2.0f;
    private float _currentLife;

    [SerializeField]
    private float _hitDisplacement = 5;
    private bool _hit = false;

    private Transform _target;
    public Transform Target
    {
        set { _target = value; }
    }

    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        PixelsToUnits();
    }

    public void Init(Vector3 pos, Transform target)
    {
        transform.position = pos;
        _currentLife = _enemyLife;
        _target = target;
        _hit = false;
        gameObject.SetActive(true);
    }

    private void PixelsToUnits()
    {
        _upp = GameManager.Instance.Config.UPP;
        _enemySpeed = _enemySpeed * _upp;
        _hitDisplacement = _hitDisplacement * _upp;
    }
	
	void Update () {
        Vector2 direction = (_target.position - transform.position).normalized;
        Vector3 movement = direction * _enemySpeed * Time.deltaTime;
        transform.position += movement;

        _renderer.sortingOrder = -(int)transform.position.y;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_hit && col.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit(col.contacts[0].normal));
        }
    }

    IEnumerator Hit(Vector3 direction)
    {
        float displacement = 0.0f;

        _hit = true;
        while(displacement < _hitDisplacement)
        {
            Vector3 movement = direction * (_enemySpeed*5) * Time.deltaTime;
            transform.position += movement;
            displacement += movement.magnitude;
            yield return null;
        }
        _currentLife--;
        if (_currentLife == 0)
            Die();
        else
            _hit = false;

    }

    void Die()
    {
        GetComponentInParent<EnemySpawner>().DestroyEnemy(this);
    }
}

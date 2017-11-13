using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float _upp;
    [SerializeField]
    private float _enemySpeed = 100.0f;
    [SerializeField]
    private bool _randomMovement = false;
    [SerializeField]
    private float _maxRandomSpeed = 50.0f;
    private float _randomSpeed = 0.0f;
    private Vector3 _randomDirection = Vector3.zero;
    [SerializeField]
    private float _randomTime = 2.0f;
    private float _currentTime = 0.0f;

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
    private Animator _anim;
    int _enemyHitHash = Animator.StringToHash("enemyHit");

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
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
        _upp = 1.0f / GameManager.Instance.Config.PPU;
        _enemySpeed = _enemySpeed * _upp;
        _hitDisplacement = _hitDisplacement * _upp;
    }
	
	void Update ()
    {
        if (GameManager.Instance.State == GameManager.GameState.GAMEPLAY)
        {
            if (_target != null)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                Vector3 movement = direction * _enemySpeed * Time.deltaTime;

                // Random movement
                _currentTime += Time.deltaTime;
                if (_randomMovement)
                {
                    if (_currentTime >= _randomTime)
                    {
                        _randomDirection = Random.insideUnitCircle.normalized;
                        _randomSpeed = Random.Range(0.0f, _maxRandomSpeed);
                        _currentTime = 0.0f;
                    }
                    movement += _randomDirection * _randomSpeed * Time.deltaTime;
                }
                transform.position += movement;

                if (direction.x > 0)
                    _renderer.flipX = false;
                else
                    _renderer.flipX = true;

                _renderer.sortingOrder = (int)(-transform.position.y + _renderer.bounds.extents.y);
            }
            _renderer.sortingOrder = (int)(-transform.position.y + _renderer.bounds.extents.y);

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_hit && (col.gameObject.tag == "Bullet" || 
                      col.gameObject.tag == "Dog" || 
                      col.gameObject.tag == "Enemy"))
        {
            StartCoroutine(Hit(col.contacts[0].normal, col.gameObject.tag));
        }
    }

    IEnumerator Hit(Vector3 direction, string colType)
    {
        float displacement = 0.0f;
        Debug.Log("HIT");

        if (colType != "Enemy")
        {
            _anim.SetTrigger(_enemyHitHash);
        }
        _hit = true;

        while(displacement < _hitDisplacement)
        {
            Vector3 movement = direction * (_enemySpeed * 5) * Time.deltaTime;
            transform.position += movement;
            displacement += movement.magnitude;
            yield return null;
        }
        if (colType != "Enemy")
        {
            _currentLife--;
            if (colType == "Dog" || _currentLife == 0)
                Die();
        }
        _hit = false;

    }

    void Die()
    {
        GetComponentInParent<EnemySpawner>().DestroyEnemy(this);
    }
}

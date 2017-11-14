using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

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
    private int _enemyLife = 2;
    private int _currentLife;

    [SerializeField]
    private float _hitDisplacement = 5;
    private bool _hit = false;

    private Transform _target;

    private SpriteRenderer _renderer;
    private Animator _anim;
    int _enemyHitHash = Animator.StringToHash("enemyHit");
    int _enemyLifeHash = Animator.StringToHash("enemyLife");

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
        float upp = 1.0f / GameManager.Instance.Config.PPU;
        _enemySpeed = _enemySpeed * upp;
        _hitDisplacement = _hitDisplacement * upp;
    }
	
	void Update ()
    {
        if (GameManager.Instance.State == GameManager.GameState.GAMEPLAY)
        {
            if (_target != null && _currentLife > 0)
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
        _hit = true;
        Debug.Log("HIT");

        if (colType != "Enemy")
        {
            if(colType == "Dog")
                _currentLife = 0;
            else
                _currentLife--;

            Debug.Log(_currentLife);
            _anim.SetInteger(_enemyLifeHash, _currentLife);
            _anim.SetTrigger(_enemyHitHash);
        }

        while(displacement < _hitDisplacement)
        {
            Vector3 movement = direction * (_enemySpeed * 5) * Time.deltaTime;
            transform.position += movement;
            displacement += movement.magnitude;
            yield return null;
        }
        if (_currentLife == 0)
        {
            yield return new WaitForSeconds(1.0f);
            Die();
        }
        else
            _hit = false;

    }

    void Die()
    {
        Debug.Log("Death");
        GetComponentInParent<EnemySpawner>().DestroyEnemy(this);
    }
}

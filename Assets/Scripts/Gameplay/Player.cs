using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private enum PlayerState
    {
        IDLE,
        RUNNING
    }

    [SerializeField]
    private float _playerSpeed = 150;
    [SerializeField]
    private float _hitDisplacement = 5.0f;
    private bool _hit = false;
    private PlayerState _state = PlayerState.IDLE;
    [SerializeField]
    private float _dustSpawnTime = 0.2f;
    private float _dustCurrentTime = 0.0f;
    [SerializeField]
    Transform _dustSpawnPoint;

    [SerializeField]
    private int _maxLife = 10;
    private int _currentLife = 10;

    public int MaxLife
    {
        get { return _maxLife; }
    }
    public int CurrentLife
    {
        get { return _currentLife; }
    }

    [Space(20)]
    [SerializeField]
    private Aim _aim;
    [SerializeField]
    private Slingshot _slingshot;
    [SerializeField]
    private Bullet _bulletPrefab;
    private Pool<Bullet> _bulletPool;
    private bool _canShoot = true;

    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private Animator _anim;
    int _playerSpeedHash = Animator.StringToHash("playerSpeed");
    int _playerHitHash = Animator.StringToHash("playerHit");

    private ShootEvent _shootEvent;

    private int _ppu;

	void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _shootEvent = new ShootEvent();
        _bulletPool = new Pool<Bullet>(20, _bulletPrefab, gameObject);
    }

    void Start()
    {
        PixelsToUnits();
    }

    private void PixelsToUnits()
    {
        float upp = 1.0f / GameManager.Instance.Config.PPU;
        _playerSpeed = _playerSpeed * upp;
        _hitDisplacement = _hitDisplacement * upp;
    }
	
	void FixedUpdate () {
        if (GameManager.Instance.State == GameManager.GameState.GAMEPLAY && !_hit)
        {
            // MOVEMENT
            float axis_h = Input.GetAxisRaw("Horizontal");
            float axis_v = Input.GetAxisRaw("Vertical");

            Vector2 movement = Vector2.zero;
            float deltaSpeed = _playerSpeed * Time.deltaTime;

            if (axis_h < 0)
                movement.x -= deltaSpeed;
            if (axis_h > 0)
                movement.x += deltaSpeed;
            if (axis_v < 0)
                movement.y -= deltaSpeed;
            if (axis_v > 0)
                movement.y += deltaSpeed;

            _rb.MovePosition(_rb.position + movement);

            _anim.SetFloat(_playerSpeedHash, movement.sqrMagnitude);
            UpdateState(movement.sqrMagnitude, axis_h < 0);

            if (_aim.transform.position.x < transform.position.x)
                _renderer.flipX = true;
            else
                _renderer.flipX = false;
            _renderer.sortingOrder = (int)(-transform.position.y + _renderer.bounds.extents.y);


            // WEAPON SYSTEM
            _aim.CustomUpdate();
            _slingshot.CustomUpdate(_renderer.sortingOrder + 1);

            // SHOOTING
            float shoot = 0.0f;
            if (GameManager.Instance.Settings.controller)
                shoot = Input.GetAxisRaw("Shoot");
            else
                shoot = Input.GetMouseButtonDown(0) ? 1.0f : 0.0f;

            if (shoot != 0.0f)
            {
                if (_canShoot)
                {
                    _canShoot = false;
                    CreateBullet();
                    EventManager.Instance.OnEvent(this, _shootEvent);
                }
            }
            else
            {
                _canShoot = true;
            }
        }
    }

    void UpdateState(float movementSqrMagnitude, bool flipX)
    {
        if (_state == PlayerState.IDLE)
        {
            if (movementSqrMagnitude >= 0.001f)
                _state = PlayerState.RUNNING;
        }
        else if (_state == PlayerState.RUNNING)
        {
            if (movementSqrMagnitude >= 0.001f)
                _dustCurrentTime += Time.deltaTime;
            else
            {
                _state = PlayerState.IDLE;
                _dustCurrentTime = 0.0f;
            }
        }
        if (_dustCurrentTime >= _dustSpawnTime)
        {
            //Spawn dust
            EffectsManager.Instance.CreateEffect(EffectsManager.EffectType.DUST, _dustSpawnPoint.position, flipX);
            _dustCurrentTime = 0.0f;
        }
    }

    void CreateBullet()
    {
        Bullet bullet = _bulletPool.CreateObject();
        Vector3 direction = (_aim.transform.position - transform.position).normalized;
        bullet.Init(transform.position + 2 * direction, direction);
    }

    public void DestroyBullet(Bullet bullet)
    {
        _bulletPool.DestroyObject(bullet);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_hit && col.gameObject.tag == "Enemy")
        {
            StartCoroutine(Hit(col.contacts[0].normal));
        }
    }

    IEnumerator Hit(Vector2 direction)
    {
        float displacement = 0.0f;

        _anim.SetTrigger(_playerHitHash);
        _hit = true;

        _currentLife--;

        while (displacement < _hitDisplacement)
        {
            Vector2 movement = direction * (_playerSpeed * 2) * Time.deltaTime;
            _rb.MovePosition(_rb.position + movement);
            displacement += movement.magnitude;
            yield return null;
        }

        if (_currentLife == 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            _hit = false;
        }
    }
}

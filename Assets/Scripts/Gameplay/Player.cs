using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float _upp;
    [SerializeField]
    private float _playerSpeed = 150;
    [SerializeField]
    private float _maxXMovement = 100;
    [SerializeField]
    private float _maxYMovement = 50;
    [SerializeField]
    private float _hitDisplacement = 5.0f;
    private bool _hit = false;

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

    private SpriteRenderer _renderer;
    private Animator _anim;
    int _playerSpeedHash = Animator.StringToHash("playerSpeed");
    int _playerHitHash = Animator.StringToHash("playerHit");

    private ShootEvent _shootEvent;

    private int _ppu;

	void Awake()
    {
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
        _upp = 1.0f / GameManager.Instance.Config.PPU;
        _playerSpeed = _playerSpeed * _upp;
        _maxXMovement = _maxXMovement * _upp;
        _maxYMovement = _maxYMovement * _upp;
        _hitDisplacement = _hitDisplacement * _upp;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.State == GameManager.GameState.GAMEPLAY)
        {
            // MOVEMENT
            float axis_h = Input.GetAxisRaw("Horizontal");
            float axis_v = Input.GetAxisRaw("Vertical");

            Vector3 movement = Vector3.zero;
            float deltaSpeed = _playerSpeed * Time.deltaTime;

            if (axis_h < 0)
                movement.x -= deltaSpeed;
            if (axis_h > 0)
                movement.x += deltaSpeed;
            if (axis_v < 0)
                movement.y -= deltaSpeed;
            if (axis_v > 0)
                movement.y += deltaSpeed;

            _anim.SetFloat(_playerSpeedHash, movement.sqrMagnitude);

            Vector3 newPos = transform.position;
            newPos += movement;
            newPos.x = Mathf.Clamp(newPos.x, -_maxXMovement, _maxXMovement);
            newPos.y = Mathf.Clamp(newPos.y, -_maxYMovement, _maxYMovement);
            transform.position = newPos;

            if (_aim.transform.position.x < transform.position.x)
                _renderer.flipX = true;
            else
                _renderer.flipX = false;
            _renderer.sortingOrder = -(int)transform.position.y;


            // WEAPON SYSTEM
            _aim.CustomUpdate();
            _slingshot.CustomUpdate();

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

    void CreateBullet()
    {
        Bullet bullet = _bulletPool.CreateObject();
        bullet.Init(transform.position, (_aim.transform.position - transform.position).normalized);
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

    IEnumerator Hit(Vector3 direction)
    {
        float displacement = 0.0f;

        _anim.SetTrigger(_playerHitHash);
        _hit = true;

        while (displacement < _hitDisplacement)
        {
            Vector3 movement = direction * (_playerSpeed * 2) * Time.deltaTime;
            transform.position += movement;
            displacement += movement.magnitude;
            yield return null;
        }
        _hit = false;

    }
}

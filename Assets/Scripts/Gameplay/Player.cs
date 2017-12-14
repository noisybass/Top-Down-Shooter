using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {

    private enum PlayerState
    {
        IDLE,
        RUNNING,
        DEATH
    }
    private PlayerState _state = PlayerState.IDLE;

    [SerializeField]
    private float _playerSpeed = 150;

    [SerializeField]
    private float _hitDisplacement = 5.0f;
    private bool _hit = false;

    [SerializeField]
    private float _dustSpawnTime = 0.2f;
    private float _dustCurrentTime = 0.0f;
    [SerializeField]
    Transform _dustSpawnPoint;

    [SerializeField]
    private int _maxLife = 10;
    public int MaxLife { get { return _maxLife; } }
    private int _currentLife = 10;
    public int CurrentLife { get { return _currentLife; } }

    private WeaponSystem _weaponSystem;
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private Animator _anim;
    int _playerSpeedHash = Animator.StringToHash("playerSpeed");
    int _playerHitHash = Animator.StringToHash("playerHit");
    int _playerLifeHash = Animator.StringToHash("playerLife");

	void Awake()
    {
        _weaponSystem = GetComponentInChildren<WeaponSystem>();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
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
            Vector2 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 movement = input.normalized * _playerSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + movement);

            _anim.SetFloat(_playerSpeedHash, movement.sqrMagnitude);
            _renderer.sortingOrder = (int)(-transform.position.y + _renderer.bounds.extents.y);

            // STATE
            UpdateState(movement.sqrMagnitude, input.x < 0);

            // WEAPON SYSTEM
            _weaponSystem.CustomUpdate(_renderer.sortingOrder);

            // UPDATE FACING
            if (_weaponSystem.AimPosition().x < transform.position.x)
                _renderer.flipX = true;
            else
                _renderer.flipX = false;
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_hit && col.gameObject.tag == "Enemy")
        {
            StartCoroutine(Hit(col.contacts[0].normal));
            SoundManager.Instance.PlayPlayerHit();
        }
    }

    IEnumerator Hit(Vector2 direction)
    {
        float displacement = 0.0f;
        _hit = true;
        _currentLife--;
        _anim.SetInteger(_playerLifeHash, _currentLife);
        _anim.SetTrigger(_playerHitHash);

        if (_currentLife == 0)
        {
            _weaponSystem.gameObject.SetActive(false);
        }

        while (displacement < _hitDisplacement)
        {
            Vector2 movement = direction * (_playerSpeed * 2) * Time.deltaTime;
            _rb.MovePosition(_rb.position + movement);
            displacement += movement.magnitude;
            yield return null;
        }

        if (_currentLife == 0)
        {
            _state = PlayerState.DEATH;
            GameManager.Instance.GameOver();
        }
        else
        {
            _hit = false;
        }
    }
}

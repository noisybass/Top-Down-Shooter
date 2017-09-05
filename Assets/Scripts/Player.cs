﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _playerSpeed = 2.0f;
    [SerializeField]
    private float _maxXMovement = 5.0f;
    [SerializeField]
    private float _maxYMovement = 4.0f;
    [SerializeField]
    private Aim _aim;
    [SerializeField]
    private Bullet _bulletPrefab;
    private Pool<Bullet> _bulletPool;
    private bool _canShoot = true;

    private SpriteRenderer _renderer;

    private ShootEvent _shootEvent;

	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _shootEvent = new ShootEvent();
        _bulletPool = new Pool<Bullet>(20, _bulletPrefab, gameObject);
    }
	
	// Update is called once per frame
	void Update () {
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

        Vector3 newPos = transform.position;
        newPos += movement;
        newPos.x = Mathf.Clamp(newPos.x, -_maxXMovement, _maxXMovement);
        newPos.y = Mathf.Clamp(newPos.y, -_maxYMovement, _maxYMovement);
        transform.position = newPos;

        if (_aim.transform.position.x < transform.position.x)
            _renderer.flipX = true;
        else
            _renderer.flipX = false;

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

    void CreateBullet()
    {
        Bullet bullet = _bulletPool.CreateObject();
        bullet.Init(transform.position, _aim.transform.position - transform.position);
    }

    public void DestroyBullet(Bullet bullet)
    {
        _bulletPool.DestroyObject(bullet);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
            Debug.Log("AY");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float _bulletSpeed = 10;

    [SerializeField]
    private float _timeToLife = 2.0f;
    private float _currentTime = 0.0f;

    private Vector2 _direction;
    public Vector2 Direction
    {
        set { _direction = value; }
    }

    private Slingshot _slingshot;

    void Start()
    {
        PixelsToUnits();
    }

    private void PixelsToUnits()
    {
        float upp = 1.0f / GameManager.Instance.Config.PPU;
        _bulletSpeed = _bulletSpeed * upp;
    }

    public void Init(Vector3 pos, Vector2 direction, Slingshot slingshot)
    {
        transform.position = pos;
        _direction = direction;
        _currentTime = 0.0f;
        _slingshot = slingshot;
        gameObject.SetActive(true);
    }

	void Update ()
    {
        if (GameManager.Instance.State == GameManager.GameState.GAMEPLAY)
        {
            Vector3 movement = _direction * _bulletSpeed * Time.deltaTime;
            transform.position += movement;

            _currentTime += Time.deltaTime;
            if (_currentTime >= _timeToLife)
                DestroyBullet();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Block")
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        _slingshot.DestroyBullet(this);
    }
}

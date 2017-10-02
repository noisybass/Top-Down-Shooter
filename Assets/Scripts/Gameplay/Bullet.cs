using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float _upp;
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

    void Start()
    {
        PixelsToUnits();
    }

    private void PixelsToUnits()
    {
        _upp = GameManager.Instance.Config.UPP;
        _bulletSpeed = _bulletSpeed * _upp;
    }

    public void Init(Vector3 pos, Vector2 direction)
    {
        transform.position = pos;
        _direction = direction;
        _currentTime = 0.0f;
        gameObject.SetActive(true);
    }

	void Update () {
        Vector3 movement = _direction * _bulletSpeed * Time.deltaTime;
        transform.position += movement;

        _currentTime += Time.deltaTime;
        if (_currentTime >= _timeToLife)
            Die();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    void Die()
    {
        GetComponentInParent<Player>().DestroyBullet(this);
    }
}

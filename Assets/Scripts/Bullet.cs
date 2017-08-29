using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float _bulletSpeed = 2.0f;

    private Vector2 _direction;
    public Vector2 Direction
    {
        set { _direction = value; }
    }

	void Update () {
        Vector3 movement = _direction * _bulletSpeed * Time.deltaTime;
        transform.position += movement;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

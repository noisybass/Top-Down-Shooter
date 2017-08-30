using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float _enemySpeed = 2.0f;

    private Transform _target;
    public Transform Target
    {
        set { _target = value; }
    }
	
	void Update () {
        Vector2 direction = (_target.position - transform.position).normalized;
        Vector3 movement = direction * _enemySpeed * Time.deltaTime;
        transform.position += movement;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GetComponentInParent<EnemySpawner>().DestroyEnemy(this);
        }
    }
}

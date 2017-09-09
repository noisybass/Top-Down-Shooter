using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private Aim _aim;

    private SpriteRenderer _renderer;

	void Awake () {
        _renderer = GetComponentInChildren<SpriteRenderer>();
	}
	
	void LateUpdate () {
        float rotationZ = Mathf.Atan2(_aim.Direction.y, _aim.Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (_aim.Direction.y > 0.2f)
        {
            _renderer.sortingOrder = -1;
        }
        else
        {
            _renderer.sortingOrder = 1;
        }

        if (rotationZ <= 90 && rotationZ >= -90)
        {
            _renderer.flipY = false;
        }
        else
        {
            _renderer.flipY = true;
        }

    }
}

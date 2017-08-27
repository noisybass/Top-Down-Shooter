using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private Aim _aim;

    private SpriteRenderer _renderer;

	void Awake () {
        _renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_aim.Direction.y > 0.2f)
            _renderer.sortingOrder = -1;
        else
            _renderer.sortingOrder = 1;

        if (_aim.transform.position.x < transform.position.x)
            _renderer.flipY = true;
        else
            _renderer.flipY = false;

        float rotationZ = Mathf.Atan2(_aim.Direction.y, _aim.Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}

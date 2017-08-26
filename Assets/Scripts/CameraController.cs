using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _aim;

    private Vector3 _distanceToPlayer;
    private Vector3 _offset;

    private float _maxOffsetX;
    private float _maxOffsetY;
    [SerializeField]
    private float _maxOffsetXFactor = 0.6f;
    [SerializeField]
    private float _maxOffsetYFactor = 0.6f;

	// Use this for initialization
	void Start () {
        _distanceToPlayer = transform.position - _player.transform.position;
        Vector2 maxOffset = Camera.main.ScreenToWorldPoint(Camera.main.pixelRect.max);
        _maxOffsetX = maxOffset.x * _maxOffsetXFactor;
        _maxOffsetY = maxOffset.y * _maxOffsetYFactor;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        _offset = (_aim.transform.position - _player.transform.position) * 0.5f;
        if (Mathf.Abs(_offset.x) > _maxOffsetX)
        {
            float p = _offset.x / _offset.y;
            _offset.x = Mathf.Clamp(_offset.x, -_maxOffsetX, _maxOffsetX);
            _offset.y = _offset.x / p;
        }
        if (Mathf.Abs(_offset.y) > _maxOffsetY)
        {
            float p = _offset.y / _offset.x;
            _offset.y = Mathf.Clamp(_offset.y, -_maxOffsetY, _maxOffsetY);
            _offset.x = _offset.y / p;
        }
        transform.position = _player.transform.position + _offset + _distanceToPlayer;
	}
}

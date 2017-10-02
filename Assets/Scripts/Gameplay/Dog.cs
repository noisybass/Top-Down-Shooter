using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    private float _upp;
    private float _dogSpeed = 16;

    private SpriteRenderer _renderer;

	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

	void Start () {
        PixelsToUnits();
	}

    private void PixelsToUnits()
    {
        _upp = GameManager.Instance.Config.UPP;
        _dogSpeed = _dogSpeed * _upp;
    }

    void Update()
    {
        _renderer.sortingOrder = -(int)transform.position.y;
    }
}

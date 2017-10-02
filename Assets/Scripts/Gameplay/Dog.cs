using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    private float _upp;
    private float _dogSpeed = 16;

	// Use this for initialization
	void Start () {
        PixelsToUnits();
	}

    private void PixelsToUnits()
    {
        _upp = GameManager.Instance.Config.UPP;
        _dogSpeed = _dogSpeed * _upp;
    }

}

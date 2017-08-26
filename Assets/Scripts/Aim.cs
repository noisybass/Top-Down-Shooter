using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    private Camera _cameraMain;

	void Awake()
    {
        _cameraMain = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 worldPos = new Vector3();
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, _cameraMain.pixelWidth);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, _cameraMain.pixelHeight);
        worldPos = _cameraMain.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        transform.position = worldPos;
    }
}

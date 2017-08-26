using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    [SerializeField]
    private bool _controller = false;

    [Header("Controller Configuration")]
    [SerializeField]
    private float _controllerSpeed = 7.0f;
    [SerializeField]
    private float _controllerRadius = 3.0f;
    [SerializeField]
    private float _controllerMinRadiusFactor = 0.3f;

    [SerializeField]
    private GameObject _player;

    private Camera _cameraMain;

    private Vector2 _direction = new Vector2(0, 1);

	void Awake()
    {
        _cameraMain = Camera.main;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (_controller)
        {
            ControllerUpdate();
        }
        else
        {
            MouseUpdate();
        }
    }

    void MouseUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, _cameraMain.pixelWidth);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, _cameraMain.pixelHeight);

        Vector3 worldPos = Vector3.zero;
        worldPos = _cameraMain.ScreenToWorldPoint(mousePos);
        worldPos.z = 0.0f;

        transform.position = worldPos;
    }

    /*
        x = r cos + player_x
        y = r sin + player_y
     */
    void ControllerUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("RightJoystickX"), Input.GetAxisRaw("RightJoystickY"));
        input.y = -input.y;
        if (input.magnitude > _controllerMinRadiusFactor)
            _direction = input;

        input = _direction * Mathf.Max(_controllerMinRadiusFactor, input.magnitude);
        Debug.Log(Mathf.Max(_controllerMinRadiusFactor, input.magnitude));
        
        Vector3 worldPos = Vector3.zero;
        worldPos.x = _controllerRadius * input.x + _player.transform.position.x;
        worldPos.y = _controllerRadius * input.y + _player.transform.position.y;

        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * _controllerSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

    [Header("Controller Configuration")]
    [SerializeField]
    private float _controllerSpeed = 7.0f;
    [SerializeField]
    private float _controllerRadius = 3.0f;
    [SerializeField]
    private float _minInputMagnitude = 0.3f;

    [SerializeField]
    private GameObject _player;

    private Camera _cameraMain;

    private Vector2 _direction = new Vector2(1, 0);
    public Vector2 Direction
    {
        get { return _direction; }
    }

	void Awake()
    {
        _cameraMain = Camera.main;
    }
	
	public void CustomUpdate () {
        if (GameManager.Instance.Settings.controller)
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
        _direction = new Vector2(worldPos.x, worldPos.y).normalized;

        transform.position = worldPos;

        Vector3 playerToAim = transform.position - _player.transform.position;
        float angle = Mathf.Atan2(playerToAim.y, playerToAim.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    /*
        x = r cos + player_x
        y = r sin + player_y
     */
    void ControllerUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("RightJoystickX"), Input.GetAxisRaw("RightJoystickY"));
        input.y = -input.y;
        if (input.magnitude > _minInputMagnitude)
        {
            _direction = input;
            _direction.Normalize();
        }

        input = _direction * Mathf.Max(_minInputMagnitude, input.magnitude);
                
        Vector3 worldPos = Vector3.zero;
        worldPos.x = _controllerRadius * input.x + _player.transform.position.x;
        worldPos.y = _controllerRadius * input.y + _player.transform.position.y;

        transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * _controllerSpeed);

        Vector3 playerToAim = transform.position - _player.transform.position;
        float angle = Mathf.Atan2(playerToAim.y, playerToAim.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_player.transform.position, _minInputMagnitude * _controllerRadius);
        Gizmos.DrawWireSphere(_player.transform.position, _controllerRadius);
    }
}

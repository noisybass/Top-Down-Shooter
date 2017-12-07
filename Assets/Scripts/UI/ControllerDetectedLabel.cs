using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDetectedLabel : MonoBehaviour {

	void OnEnable()
    {
        if (!GameManager.Instance.IsControllerConnected())
            gameObject.SetActive(false);
    }
}

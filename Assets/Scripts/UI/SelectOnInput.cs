using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public GameObject selectedObject;

    private EventSystem eventSystem;
    private bool buttonSelected = false;

    void Awake()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();    
    }

	void Update ()
    {
		if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableUIElement : MonoBehaviour, IPointerEnterHandler {

    private SelectOnInput panel;

    private void Start()
    {
        panel = GameObject.FindGameObjectWithTag("SelectOnInputPanel").GetComponent<SelectOnInput>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.Select(gameObject);
    }
}

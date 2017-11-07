using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogHealthBar : MonoBehaviour {

    [SerializeField]
    private Dog dog;

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = (float)dog.CurrentLife / dog.MaxLife;
    }
}

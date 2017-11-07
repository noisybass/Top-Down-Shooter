using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private Text label;

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Update()
    {
        label.text = "SCORE: " + GameManager.Instance.Data.score.ToString();
    }
}

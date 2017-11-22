using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour {

    private Text label;

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Update()
    {
        label.text = "HIGHSCORE: " + GameManager.Instance.Data.highScore.ToString();
    }
}

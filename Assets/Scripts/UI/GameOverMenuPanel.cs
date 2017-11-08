using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuPanel : MonoBehaviour {

	public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void BackToStartMenu()
    {
        GameManager.Instance.BackToStartMenu();
    }
}

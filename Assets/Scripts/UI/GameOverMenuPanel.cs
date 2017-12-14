using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuPanel : MonoBehaviour {

	public void RestartGame()
    {
        GameManager.Instance.RestartGame();
        SoundManager.Instance.PlayButton();
    }

    public void BackToStartMenu()
    {
        GameManager.Instance.BackToStartMenu();
        SoundManager.Instance.PlayButton();
    }
}

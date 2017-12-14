using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPanel : MonoBehaviour {

	public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        SoundManager.Instance.PlayButton();
    }

    public void OpenSettings()
    {
        GameManager.Instance.OpenSettings();
        SoundManager.Instance.PlayButton();
    }

    public void BackToStartMenu()
    {
        GameManager.Instance.BackToStartMenu();
        SoundManager.Instance.PlayButton();
    }
}

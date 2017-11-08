using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPanel : MonoBehaviour {

	public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OpenSettings()
    {
        GameManager.Instance.OpenSettings();
    }

    public void BackToStartMenu()
    {
        GameManager.Instance.BackToStartMenu();
    }
}

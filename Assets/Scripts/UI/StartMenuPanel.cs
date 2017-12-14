using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuPanel : MonoBehaviour {

	public void StartGame()
    {
        GameManager.Instance.StartGame();
        SoundManager.Instance.PlayButton();
    }

    public void OpenSettings()
    {
        GameManager.Instance.OpenSettings();
        SoundManager.Instance.PlayButton();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

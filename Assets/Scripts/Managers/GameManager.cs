using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public enum GameState
    {
        START_MENU,
        PAUSE_MENU,
        SETTINGS_MENU,
        GAMEPLAY,
        GAME_OVER
    }

    [System.Serializable]
    public struct GraphicsConfigData
    {
        public float PPU;
        [Range(1, 4)]
        public int pixelScale;
    }

    [System.Serializable]
    public struct SettingsData
    {
        public bool controller;
    }

    [System.Serializable]
    public struct GameplayData
    {
        public int highScore;
        public int score;
    }

    [SerializeField]
    private GraphicsConfigData _config;
    public GraphicsConfigData Config
    {
        get { return _config; }
    }

    [SerializeField]
    private SettingsData _settings;
    public SettingsData Settings
    {
        get { return _settings; }
    }

    private GameplayData _data;
    public GameplayData Data
    {
        get { return _data; }
    }

    private GameState _state;
    private GameState _previousState;
    public GameState State
    {
        get { return _state; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    void Start ()
    {
        _state = _previousState = GameState.START_MENU;
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (_state == GameState.GAMEPLAY && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            _previousState = _state;
            _state = GameState.PAUSE_MENU;
        }
    }

    // From START menu
    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        _previousState = _state;
        _state = GameState.GAMEPLAY;
    }

    // From GAME_OVER menu
    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync("GameOverMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        _previousState = _state;
        _state = GameState.GAMEPLAY;
    }

    // From PAUSE menu
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        _previousState = _state;
        _state = GameState.GAMEPLAY;
        Time.timeScale = 1;
    }

    // From PAUSE menu or GAME_OVER menu
    public void BackToStartMenu()
    {
        if (_state == GameState.PAUSE_MENU)
            SceneManager.UnloadSceneAsync("PauseMenu");
        else
        {
            SceneManager.UnloadSceneAsync("GameOverMenu");
            SceneManager.UnloadSceneAsync("Gameplay");
        }
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
        _previousState = _state;
        _state = GameState.START_MENU;
    }

    // From START menu or PAUSE menu
    public void OpenSettings()
    {
        if (_state == GameState.START_MENU)
            SceneManager.UnloadSceneAsync("StartMenu");
        else
            SceneManager.UnloadSceneAsync("PauseMenu");
        SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
        _previousState = _state;
        _state = GameState.SETTINGS_MENU;
    }

    // From SETTINGS menu
    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync("SettingsMenu");
        if (_previousState == GameState.START_MENU)
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
            _previousState = _state;
            _state = GameState.START_MENU;
        }
        else
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            _previousState = _state;
            _state = GameState.PAUSE_MENU;
        }

    }
}

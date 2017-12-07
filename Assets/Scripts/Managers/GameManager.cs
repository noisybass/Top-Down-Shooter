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

        _data.score = 0;
        if (PlayerPrefs.HasKey("highScore"))
            _data.highScore = PlayerPrefs.GetInt("highScore");
        else
            _data.highScore = 0;

        if (IsControllerConnected())
        {
            if (PlayerPrefs.HasKey("controller"))
            {
                if (PlayerPrefs.GetInt("controller") == 1)
                    _settings.controller = true;
                else
                    _settings.controller = false;
            }
            else
                _settings.controller = true;
        }
        else
            _settings.controller = false;
    }

    void Start ()
    {
        _state = _previousState = GameState.START_MENU;
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }

    private void Update()
    {
        // GAMEPLAY -> PAUSE
        if (_state == GameState.GAMEPLAY && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            Cursor.visible = true;
            _previousState = _state;
            _state = GameState.PAUSE_MENU;
        }
    }

    public bool IsControllerConnected()
    {
        return Input.GetJoystickNames().Length != 0;
    }

    public void AddScore()
    {
        _data.score++;
    }

    // START -> GAMEPLAY
    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("StartMenu");
        _data.score = 0;
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        Cursor.visible = false;
        _previousState = _state;
        _state = GameState.GAMEPLAY;
    }

    // GAME_OVER -> GAMEPLAY
    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync("GameOverMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
        _data.score = 0;
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        _previousState = _state;
        _state = GameState.GAMEPLAY;
    }

    // PAUSE -> GAMEPLAY
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        Cursor.visible = false;
        _previousState = _state;
        _state = GameState.GAMEPLAY;
        Time.timeScale = 1;
    }

    // PAUSE -> MENU || GAME_OVER -> MENU
    public void BackToStartMenu()
    {
        if (_state == GameState.PAUSE_MENU)
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            SceneManager.UnloadSceneAsync("Gameplay");
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.UnloadSceneAsync("GameOverMenu");
            SceneManager.UnloadSceneAsync("Gameplay");
        }

        if (_data.score > _data.highScore)
        {
            _data.highScore = _data.score;
            PlayerPrefs.SetInt("highScore", _data.highScore);
        }

        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
        _previousState = _state;
        _state = GameState.START_MENU;
    }

    // MENU -> SETTINGS || PAUSE -> SETTINGS
    public void OpenSettings()
    {
        if (_state == GameState.START_MENU)
        {
            SceneManager.UnloadSceneAsync("StartMenu");
            SceneManager.LoadScene("SettingsMenuFromStart", LoadSceneMode.Additive);
        }  
        else
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            SceneManager.LoadScene("SettingsMenuFromPause", LoadSceneMode.Additive);
        }
        _previousState = _state;
        _state = GameState.SETTINGS_MENU;
    }

    // SETTINGS -> START || SETTINGS -> PAUSE
    public void CloseSettings()
    {
        if (_previousState == GameState.START_MENU)
        {
            SceneManager.UnloadSceneAsync("SettingsMenuFromStart");
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
            _previousState = _state;
            _state = GameState.START_MENU;
        }
        else
        {
            SceneManager.UnloadSceneAsync("SettingsMenuFromPause");
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            _previousState = _state;
            _state = GameState.PAUSE_MENU;
        }
    }

    // GAMEPLAY -> GAME_OVER
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Additive);
        Cursor.visible = true;
        _previousState = _state;
        _state = GameState.GAME_OVER;
    }

    public void ControllerSettingsChange(bool value)
    {
        _settings.controller = value;
        if (value)
            PlayerPrefs.SetInt("controller", 1);
        else
            PlayerPrefs.SetInt("controller", 0);
    }

    public void VolumeSettingsChange(float value)
    {

    }
}

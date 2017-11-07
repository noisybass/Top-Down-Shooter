using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

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

    protected override void Awake()
    {
        base.Awake();
    }

    void Start () {

    }
	
	void Update () {
		
	}
}

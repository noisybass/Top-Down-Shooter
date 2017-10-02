using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [System.Serializable]
    public struct ConfigData
    {
        public int PPU;
        public float UPP;
    }

    [System.Serializable]
    public struct SettingsData
    {
        public bool controller;
    }

    [SerializeField]
    private ConfigData _config;
    public ConfigData Config
    {
        get { return _config; }
    }

    [SerializeField]
    private SettingsData _settings;
    public SettingsData Settings
    {
        get { return _settings; }
    }

    protected override void Awake()
    {
        base.Awake();

        _config.UPP = 1.0f / _config.PPU;
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}

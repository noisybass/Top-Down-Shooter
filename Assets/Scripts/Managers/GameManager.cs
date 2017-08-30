using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [System.Serializable]
    public struct SettingsData
    {
        public bool controller;
    }

    [SerializeField]
    private SettingsData _settings;
    public SettingsData Settings
    {
        get { return _settings; }
    }

    //protected override void Awake()
    //{
    //    base.Awake();
    //}

    void Start () {
		
	}
	
	void Update () {
		
	}
}

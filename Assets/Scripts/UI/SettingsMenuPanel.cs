using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuPanel : MonoBehaviour {

	public void CloseSettings()
    {
        GameManager.Instance.CloseSettings();
    }
}

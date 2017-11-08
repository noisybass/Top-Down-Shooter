using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCamera : MonoBehaviour {

    void Awake()
    {
        GetComponent<Camera>().orthographicSize = Screen.height * 0.5f / (GameManager.Instance.Config.PPU * GameManager.Instance.Config.pixelScale);
    }
}

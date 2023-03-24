using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class InitMainCam : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.6f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class LoadVolume : MonoBehaviour
{
    private void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.6f);
    }
}

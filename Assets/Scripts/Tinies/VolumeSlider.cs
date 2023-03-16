using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Volume", 1f));
    }
    public void SetVolume()
    {
        AudioListener.volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}

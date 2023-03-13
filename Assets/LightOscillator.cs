using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOscillator : MonoBehaviour
{   
    float _currentBrightness;
    float _pastBrightness;
    Light _lightToOscillate;
    void Start()
    {
        _lightToOscillate = GetComponent<Light>();
    }

    void Update()
    {        
        // _pastBrightness = _currentBrightness;
        // _currentBrightness = _lightToOscillate.intensity;
        // float _newBrightness = _pastBrightness -

    }
}

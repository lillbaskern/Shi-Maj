using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIListener : MonoBehaviour
{
    TextMeshProUGUI _text;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        PlayerHead.CharChanged += OnCharChanged;
    }
    //i feel like with one of these classes i could probably make it use generics to work for many aspects of the UI, but it is just out of my reach at the moment
    void OnCharChanged(object sender, CharChangeEventArgs args)
    {
        _text.text = args.CharacterName;
    }
    private void OnDisable()
    {
        PlayerHead.CharChanged -= OnCharChanged;
    }
}

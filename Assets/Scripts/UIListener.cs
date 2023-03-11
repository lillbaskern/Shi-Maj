using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//observer class for 
public class UIListener : MonoBehaviour
{
    TextMeshProUGUI _text;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        PlayerHead.TextChanged += OnCharChanged;
    }
    //i feel like with one of these classes i could probably make it use generics to work for many aspects of the UI, but it is just out of my reach at the moment
    void OnCharChanged(object sender, CharChangeEventArgs args)
    {
        _text.text = args.text;
    }
    private void OnDisable()
    {
        PlayerHead.TextChanged -= OnCharChanged;
    }
}

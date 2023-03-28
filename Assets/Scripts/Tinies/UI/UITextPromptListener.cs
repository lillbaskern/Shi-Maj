using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UITextPromptArgs : EventArgs
{
    public UITextPromptArgs(string text)
    {
        Text = text;
        Duration = -1;
    }
    public UITextPromptArgs(string text, float duration)
    {
        Text = text;
        Duration = duration;
    }
    public string Text;
    public float Duration;
}

public class UITextPromptListener : MonoBehaviour
{

    TextMeshProUGUI _textField;
    void Start()
    {
        _textField = GetComponent<TextMeshProUGUI>();
        UITextPromptObserver.UITextPrompt += OnUITextPrompt;
    }
    const float _defaultPromptDuration = 1.3f;
    void OnUITextPrompt(object sender, UITextPromptArgs args)
    {
        //cant set float duration to be nullable due to how the third party's side of things is set up
        //instead anything with a duration of -1 == null;
        if (args.Duration != -1)
        {
            StartCoroutine(DisplayText(args.Duration, args.Text));
            return;
        }
        StartCoroutine(DisplayText(_defaultPromptDuration, args.Text));
    }

    float _textOpacity;

    IEnumerator DisplayText(float duration, string inputText)
    {
        _textField.text = inputText;
        //while text is not opaque, increase opacity
        //(this one could be hard because colours are not just numbers as far as coding is concerned)
        
        _textField.CrossFadeAlpha(1, 0.1f, true);

        yield return new WaitForSeconds(duration);

        _textField.CrossFadeAlpha(0, 0.5f, true);
        
        yield return null;
    }

    public void ResetPrompter()
    {
        StopAllCoroutines();
        _textField.alpha = 0f;
    }
}

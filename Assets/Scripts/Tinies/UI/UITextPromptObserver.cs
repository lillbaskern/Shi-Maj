using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UITextPromptObserver : MonoBehaviour
{
    
    public static event EventHandler<UITextPromptArgs> UITextPrompt;

    public static void SendUITextPrompt(object sender, UITextPromptArgs args)
    {
        UITextPrompt?.Invoke(sender, args);
    }
}

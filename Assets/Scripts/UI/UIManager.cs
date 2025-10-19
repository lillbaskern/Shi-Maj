using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using System;
[DefaultExecutionOrder(-10)]
public class UIManager : MonoBehaviour
{
    public static Action<int> XPChange;
    public static Action<int> LvlUpdate;


    public TextMeshProUGUI XPText;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LevelText;


    void Start()
    {
        XPChange += UpdateXP;
        LvlUpdate += UpdateLevel;
    }

    void UpdateLevel(int level)
    {
        LevelText.text = $"lvl: {level}";
    }
    void UpdateXP(int xp) 
    {
        XPText.text = $"XP {xp}%";
    }
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    [SerializeField]
    public string Name;
    public float Value;
    public float GrowthMult = 1; //how much the stat growth is multiplied by on level up
}

[Serializable]
public class Stats
{

    public Action OnLevelUp;

    public void AddXP(float xp)
    {
        XP += xp;
        if (XP >= XPToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        XPToNextLevel += 50 * Level;
        XP = 0;
        Level += 1;

        foreach (Stat stat in _stats)
        {
            stat.Value *= stat.GrowthMult;
        }

        OnLevelUp.Invoke();
    }

    public float GetStat(string name)
    {
        float value = -1;
        foreach (Stat stat in _stats)
        {
            if (stat.Name == name)
                value = stat.Value;
        }
        return value;
    }

    List<Stat> _stats = new() {
        new(){Name = "Strength", Value = 1},
        new(){Name = "MoveSpeed", Value = 1}
    };
    public float XP = 0;
    public float XPToNextLevel = 79f;
    public int Level = 1;

    public int Strength = 1;
    public int Luck = 1;
    public float JumpHeight = 1;
    public float FallSpeed = 1;
    public float AirSpeed = 1;
    public float MoveSpeed = 1;
    public float Acceleration = 1;
    public float XPGainMult = 1;
    public float CritChance = 1;
    public float CritDamage = 150; //percentage of original attack damage that a crit will do (150% base crit damage)
    public float ReloadSpeed = 1;
    public float ProjectileSize = 1f;
    public int ProjectileQuantity = 1;
    public int ExtraMagSize = 0;
}



[CreateAssetMenu(fileName ="Character Sheet")]
public class CharacterSheet : ScriptableObject
{

    public String ClassName = "Mortal";

    public List<Stat> stats = new()
    {
        new(){Name = "Strength", Value = 1},
        new(){Name = "MoveSpeed", Value = 1},
        new(){Name = "JumpHeight", Value = 1},
        new(){Name = "AirSpeed", Value = 1},
        new(){Name = "Luck", Value = 1},
        new(){Name = "Acceleration", Value = 1},
        new(){Name = "XPGainMult", Value = 1},
        new(){Name = "GoldGainMult", Value = 1},
        new(){Name = "CritChance", Value = 1},
        new(){Name = "CritDamage", Value = 150},
        new(){Name = "ReloadSpeed", Value = 1},
        new(){Name = "ProjectileSize", Value = 1},
        new(){Name = "ProjectileQuantity", Value = 1},
        new(){Name = "ExtraMagSize", Value = 0},

    };

}

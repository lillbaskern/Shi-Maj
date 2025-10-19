using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICharacter
{
    public abstract void CharacterLoop(InputHandler input);
    public abstract void CharacterInit();
    public void PickUpWeapon(Weapon weaponToPickup);
    public string GetName();
    public int GetLevel();
    public float GetXP();
    public void LevelUp();
    public void AddXP(float xp);

    public Weapon GetCurrWeapon();
}

public class Stats
{

    public Action OnLevelUp;

    public void AddXP(float xp)
    {
        XP += xp;
        if(XP >= XPToNextLevel) 
        {
            LevelUp();
        }
    }


    public void LevelUp()
    {
        XPToNextLevel += 70 * Level;
        XP = 0;
        Level += 1;
        OnLevelUp.Invoke();
    }


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

public class LifeLongWrestler : PlayerMove, ICharacter
{
    Stats _stats;
    public Weapon GetCurrWeapon() => CurrWeapon;
    public string GetName() => _name;

    private void Start()
    {
        _stats = new Stats();
        //the characters you have available are stored in a static list. they will add themselves to the list using sendtocharlist() from playermove
        SendToCharList(this);
    }
    public void CharacterInit()
    {
        //playermove uses awake so we dont need to rewrite its init to a seperate method to be called here
        //but shoot needs it though.
        base.InitShoot();
    }

    public void CharacterLoop(InputHandler input)
    {
        MoveAndTurnLoop(input.Turn, input.Move, input.Jump);
        ShootUpdate();
    }
    public override void Special()
    {
        Debug.Log("lifelong wrestler special used");
    }

    public int GetLevel()
    {
        return _stats.Level;
    }

    public float GetXP()
    {
        return _stats.XP;
    }

    public void AddXP(float xp)
    {
        _stats.XP += xp;
        if (_stats.XP > _stats.XPToNextLevel)
        {
            LevelUp();
        }

        float percentOfNextLevel = (_stats.XP / _stats.XPToNextLevel) * 100;

        UIManager.XPChange.Invoke((int) percentOfNextLevel);
    }

    public void LevelUp()
    {
        _stats.Level +=  1;
        _stats.XP = 0;
        _stats.XPToNextLevel += 100*_stats.Level;//placeholder xptonextlevel calculations
    }
}


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
    public int Strength;
    public int Luck = 1;
    public float JumpHeight;
    public float FallSpeed;
    public float AirSpeed;
    public float MoveSpeed;
    public float Acceleration;
    public float XPGain;
    public float CritChance;
    public float CritDamage = 150; //percentage of original attack damage that a crit will do (150% base crit damage)
    public float ReloadSpeed;
    public float ProjectileSize = 1f;
    public int ProjectileQuantity = 1;

}

public class LifeLongWrestler : PlayerMove, ICharacter
{
    private int _level;
    private float _xp;
    private float _xpToNextLevel;

    public Weapon GetCurrWeapon() => CurrWeapon;
    public string GetName() => _name;

    private void Start()
    {
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
        return _level;
    }

    public float GetXP()
    {
        return _xp;
    }

    public void AddXP(float xp)
    {
        _xp += xp;
        if (_xp > _xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {

    }
}


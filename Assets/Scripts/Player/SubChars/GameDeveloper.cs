using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameDeveloper : PlayerMove, ICharacter
{
    public Weapon GetCurrWeapon() => CurrWeapon;
    public string GetName() => _name;
    
    Stats _stats;

    float _xpToNextLevel;
    float _xp;
    int _level;

    private void Start()
    {
        SendToCharList(this);
    }
    public void CharacterInit()
    {
        base.InitShoot();
    }
    public void CharacterLoop(InputHandler input)
    {
        MoveAndTurnLoop(input.Turn, input.Move, input.Jump);
        ShootUpdate();
    }
    public override void Special()
    {
        Debug.Log("you develop a game. you launch it on steam and immediately enter debilitating debt");
    }

    public int GetLevel()
    {
        return _level;
    }

    public float GetXP()
    {
        return _xp;
    }

    public void LevelUp()
    {
        _xp = 0;

        Debug.Log("you leveled up!");
        //todo:: increase stats
        //maybe add separate float to track which stats to increase more than others
    }

    public void AddXP(float xp)
    {
        _xp += xp;
        if (_xp > _xpToNextLevel) LevelUp();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : PlayerMove, ICharacter
{
    public string GetName() => _name;
    public Weapon GetCurrWeapon() => CurrWeapon;

    Stats _stats;

    private void Start()
    {
        _stats = new();
        _stats.OnLevelUp += LevelUp;


        _stats.MoveSpeed = 1.1f;
        float percentOfNextLevel = (_stats.XP / _stats.XPToNextLevel) * 100;

        Debug.Log(percentOfNextLevel);
        Debug.Log(_stats.XP);


        SendToCharList(this);
        UIManager.XPChange.Invoke((int)percentOfNextLevel);
        UIManager.LvlUpdate.Invoke(_stats.Level);
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
        Debug.Log("soldier special used");
    }

    public int GetLevel()
    {
        throw new System.NotImplementedException();
    }

    public float GetXP()
    {
        throw new System.NotImplementedException();
    }

    public void AddXP(float xp)
    {
        _stats.AddXP(xp);

        //_stats.XP += xp;
        //if (_stats.XP > _stats.XPToNextLevel)
        //{
        //    LevelUp();
        //}

        float percentOfNextLevel = (_stats.XP / _stats.XPToNextLevel) * 100;

        UIManager.XPChange.Invoke((int)percentOfNextLevel);
        UIManager.LvlUpdate(_stats.Level);
    }

    public void LevelUp()
    {
        _stats.ExtraMagSize += 2;

        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i] == null) continue; 
            _weapons[i].IncreaseMagSize(_stats.ExtraMagSize);
        }

        UIManager.LvlUpdate(_stats.Level);
    }
}

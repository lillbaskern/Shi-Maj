using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameDeveloper : PlayerMove, ICharacter
{
    public Weapon CurrWeapon() => Weapon;
    public string Name() => _name;
    private void OnEnable()
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
}
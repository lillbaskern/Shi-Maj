using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : PlayerMove, ICharacter
{
    public string Name() => _name;
    public Weapon CurrWeapon() => Weapon;

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
        Debug.Log("soldier special used");
    }


}

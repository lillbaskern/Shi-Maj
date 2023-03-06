using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : PlayerMove, ICharacter
{
    [SerializeField] string _name = "Soldier";
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
        MoveAndTurnLoop(input.Turn, input.Move, input.Special);
        ShootUpdate();
    }
    

}

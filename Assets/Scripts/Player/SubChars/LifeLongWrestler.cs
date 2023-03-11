using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICharacter
{
    public void CharacterLoop(InputHandler input);
    public void CharacterInit();
    public void PickUpWeapon(Weapon weaponToPickup);
    public string Name();
    public Weapon CurrWeapon();
}

public class LifeLongWrestler : PlayerMove, ICharacter
{
    public Weapon CurrWeapon() => Weapon;
    public string Name() => _name;

    private void OnEnable()
    {
        //the characters you have available are stored in a static list. they will add themselves to the list using sendtocharlist() from playermove
        SendToCharList(this);
    }
    public void CharacterInit()
    {
        //playermove uses awake so we dont need to rewrite its init to a seperate method to be called here, but shoot needs it though.
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
}


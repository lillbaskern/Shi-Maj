using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : PlayerMove, ICharacter
{

    static Weapon _currWeapon;

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
        MoveAndTurnLoop(input.Turn, input.Move);
        ShootUpdate();
        if (input.Special.WasPerformedThisFrame())
        {
            base.Jump();
        }
    }
    public void PickUpWeapon(Weapon weaponToPickup)
    {
        if (weaponToPickup == base.Weapon)
        {
            //weapons have a special setter for ammostock which makes sure they stay within the limits of their max ammo capacity
            base.Weapon.AmmoStock = weaponToPickup.AmmoStock;
            return;
        }
        base.Weapon = weaponToPickup;
    }

}

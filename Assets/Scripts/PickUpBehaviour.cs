using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public WeaponData Weapon;
    Pistol _weaponToPickUp;
    private void Awake()
    {
        _weaponToPickUp = new Pistol(Weapon);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Weapon == null) Destroy(this.gameObject);
        Debug.Log("calling pickupweapon using weapondata: " + Weapon.WeaponName);
        if (other.TryGetComponent<PlayerHead>(out PlayerHead head)) head.PickUpWeapon(_weaponToPickUp);
        Destroy(this.gameObject);
    }
}

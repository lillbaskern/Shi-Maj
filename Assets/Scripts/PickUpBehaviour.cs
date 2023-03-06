using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public WeaponData Weapon;
    Pistol _weaponToPickUp;
    private void Awake() => _weaponToPickUp = new Pistol(Weapon);
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;

        if (Weapon == null) Destroy(this.gameObject);
        
        Debug.Log("calling pickupweapon using weapondata: " + Weapon.WeaponName);
        if (other.TryGetComponent<PlayerHead>(out PlayerHead head)) head.CurrentCharacter.PickUpWeapon(_weaponToPickUp);
        Destroy(this.gameObject);
    }
}

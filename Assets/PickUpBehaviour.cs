using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public WeaponData Weapon;
    private void OnTriggerEnter(Collider other)
    {
        if(Weapon == null) Destroy(this.gameObject);
        Debug.Log("calling pickupweapon using weapondata: " + Weapon.WeaponName);
        if(other.TryGetComponent<PlayerShoot>(out PlayerShoot shoot)) shoot.PickUpWeapon(Weapon);
        Destroy(this.gameObject);
    }
}

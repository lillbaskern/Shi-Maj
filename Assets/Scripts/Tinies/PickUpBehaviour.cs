using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    public WeaponData Weapon;
    private Weapon _weapon;
    private void Start() => _weapon = new(Weapon);
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;

        if (Weapon == null) Destroy(this.gameObject);
        
        Debug.Log("calling pickupweapon using weapondata: " + Weapon.WeaponName);
        if (other.TryGetComponent<PlayerHead>(out PlayerHead head)) head.CurrentCharacter.PickUpWeapon(_weapon);
        Destroy(this.gameObject);
    }
}

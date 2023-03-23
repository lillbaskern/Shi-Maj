using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Base Data")]
public class WeaponData : ScriptableObject
{
    public AudioClip ShootSound;
    public string WeaponName;
    public float ReloadTime;
    public int Damage;
    public int BulletsPerMag;
    public int MaxBulletCapacity;
    public float Range;
    public float FireRate;
    public int AmmoStock;
    public GameObject ViewmodelPrefab;
}

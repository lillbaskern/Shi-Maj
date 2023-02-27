using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon
{
    public Weapon(float reloadTime)
    {
        //TODO: MAKE THIS CONSTRUCTOR DO THINGS BASED ON A SCRIPTABLEOBJECT PROVIDED AS PARAMETER
        IsReloading = false;

        WeaponName = "PISTOL";
        Damage = 3;
        MagCapacity = 7;
        AmmoCapacity = 30;
        _reloadTime = new WaitForSeconds(reloadTime);
        CurrMag = MagCapacity;
        Range = 100f;
    }
    public bool IsReloading { get; private set; }


    //the name of the weapon. 
    public string WeaponName { get; private set; }
    //how much damage the weapon deals
    public int Damage { get; private set; }
    //how many bullets are in your current mag
    public int CurrMag;
    //how many shots you can fire before needing to reload
    public int MagCapacity { get; private set; }
    //how many shots you can carry outside of the bullets in each mag
    public int AmmoCapacity { get; private set; }
    //the weapon's range
    public float Range { get; private set; }
    //how long it will take before the player is be able to fire again after running out of bullets in their magazine
    private WaitForSeconds _reloadTime { get; set; }
    public virtual IEnumerator Reload()
    {
        if (CurrMag == MagCapacity || IsReloading) yield return null;
        Debug.Log("Reloading");
        IsReloading = true;
        yield return _reloadTime;
        CurrMag = MagCapacity;
        IsReloading = false;
        Debug.Log("DONE reloading");
        yield return null;
    }

    public virtual void Fire(PlayerShoot player, float radius, Vector3 shootPoint)
    {
        CurrMag -= 1;
        RaycastHit hit;
        if (Physics.Raycast(shootPoint, player.transform.TransformDirection(Vector3.forward), out hit, Range))
        {
            if (hit.transform.TryGetComponent<EnemyHead>(out EnemyHead enemy))
            {
                //Whenever it says "HP = damage" or something of the like, where HP and damage are both ints, it means that the HP will subtract damage from itself
                //this is because HP is a property and its setter runs a method named takedamage which simply subtracts damage from a private hp variable
                enemy.HP = Damage;
                return;
            }

            //experimental things below. The code is meant to make the fire function more lenient, more akin to how it functions in old school doom 
            //if it doesnt function as desired just scrap it and start from scratch with it, 
            //IDEA: im sure you could place a trigger on the raycasthit's point, and have some method on it return the enemy closest to the triggers center 
            Collider[] hitCollider = new Collider[1];
            int colliders = Physics.OverlapSphereNonAlloc(hit.point, radius, hitCollider);
            for (int i = 0; i < colliders; i++)
            {
                //Whenever it says "HP = damage" or something of the like, where HP and damage are both ints, it means HP -= damage, but in an obstructed way
                if (hitCollider[i].TryGetComponent<EnemyHead>(out EnemyHead head)) head.HP = Damage;
            }
        }

    }
}

public class PlayerShoot : MonoBehaviour
{
    Weapon weapon = new(1.12f);

    //these transforms are from where raycasts will fire,  
    [SerializeField] Transform _lowShootPoint;
    [SerializeField] Transform _highShootPoint;


    void Start()
    {
        if (_highShootPoint == null || _lowShootPoint == null)
        {
            Debug.LogWarning("NOT ENOUGH SHOOTPOINTS ATTACHED, PLEASE ENSURE THAT YOU HAVE INHABITED LOWSHOOTPOINT AND HIGHSHOOTPOINT WITH ANY TRANSFORM(S)");
            Debug.Log("Player will now self destruct in 10...9...");
            Destroy(this.gameObject, 10f);
        }
    }

    void Update()
    {
        //draw ray for debug purposes
        Debug.DrawRay(_lowShootPoint.position, _lowShootPoint.TransformDirection(Vector3.forward) * 100f, Color.blue);
        Debug.DrawRay(_highShootPoint.position, _highShootPoint.TransformDirection(Vector3.forward) * 100f, Color.green);

        //handle weapon related things after this guard clause
        if (weapon == null) return;
        if (weapon.CurrMag <= 0 && !weapon.IsReloading) StartCoroutine(weapon.Reload());
    }
    public void OnShootHigh(InputValue val)
    {
        //guard clauses
        if (weapon == null)
        {
            Debug.Log("no weapon equipped. will not fire");
            return;
        }
        if (weapon.IsReloading) return;
        //after this, go!

        weapon.Fire(this, 10f, _highShootPoint.position);
        Debug.Log("fired weapon high: " + weapon.WeaponName + ". " + "Ammo left: " + weapon.CurrMag);
    }
    public void OnShootLow(InputValue val)
    {
        if (weapon == null)
        {
            Debug.Log("no weapon equipped. will not fire");
            return;
        }
        if (weapon.IsReloading) return;


        weapon.Fire(this, 5f, _lowShootPoint.position);
        Debug.Log("fired weapon low: " + weapon.WeaponName + ". " + "Ammo left: " + weapon.CurrMag);
    }
}

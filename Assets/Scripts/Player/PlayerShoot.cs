using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Weapon
{
    public Weapon(WeaponData input)
    {
        //TODO: MAKE THIS CONSTRUCTOR DO THINGS BASED ON A SCRIPTABLEOBJECT PROVIDED AS PARAMETER
        IsReloading = false;

        WeaponName = input.WeaponName;
        Damage = input.Damage;
        MagCapacity = input.BulletsPerMag;
        AmmoCapacity = input.MaxBulletCapacity;
        _reloadTime = new WaitForSeconds(input.ReloadTime);
        CurrMag = MagCapacity;
        Range = input.Range;
    }

    public bool IsReloading { get; protected set; }
    public string WeaponName { get; private set; }
    public int Damage { get; private set; }

    //how many bullets are in your current mag
    public int CurrMag;

    //how many shots you can fire before needing to reload
    public int MagCapacity { get; private set; }

    //how many shots you can carry outside of the bullets in each mag
    public int AmmoCapacity { get; private set; }
    public float Range { get; private set; }
    private WaitForSeconds _reloadTime { get; set; }


    public virtual IEnumerator Reload()
    {
        if (CurrMag == MagCapacity || IsReloading) yield return null;

        Debug.Log("Reloading");
        IsReloading = true;

        yield return _reloadTime;

        CurrMag = MagCapacity;
        IsReloading = false;

        yield return null;
        Debug.Log("DONE reloading");
    }
    //how the fuck would you 
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
    InputHandler inputHandler;
    Transform _highCrosshair;
    [SerializeField] GameObject _UiHighCrosshair;
    public Weapon Weapon { get; private set; }

    //these transforms are from where raycasts will fire
    [SerializeField] Transform _lowShootPoint;
    [SerializeField] Transform _highShootPoint;


    void Start()
    {
        _lowShootPoint = GameObject.Find("LowShootOrigin").transform;
        _highShootPoint = GameObject.Find("HighShootOrigin").transform;
        _highCrosshair = GameObject.Find("HighCrosshairDecal").transform;
        
        if (_highShootPoint == null || _lowShootPoint == null)
        {
            Debug.LogWarning("NOT ENOUGH SHOOTPOINTS ATTACHED, PLEASE ENSURE THAT YOU HAVE INHABITED LOWSHOOTPOINT AND HIGHSHOOTPOINT WITH ANY TRANSFORM(S)");
            Debug.LogWarning("Player will now self destruct in 10...9...");
            this.gameObject.SetActive(false);
        }

        _UiHighCrosshair = GameObject.Find("HIGHcrosshair");
    }

    void Update()
    {
        
        //draw ray for debug purposes
        Debug.DrawRay(_lowShootPoint.position, _lowShootPoint.TransformDirection(Vector3.forward) * 100f, Color.blue);
        Debug.DrawRay(_highShootPoint.position, _highShootPoint.TransformDirection(Vector3.forward) * 100f, Color.green);

        ProjectHighCrossHair();

        //handle weapon related things after this guard clause
        if (Weapon == null) return;
        if (Weapon.CurrMag <= 0 && !Weapon.IsReloading) StartCoroutine(Weapon.Reload());
    }


    private void ProjectHighCrossHair()
    {
        //if (_highCrosshair == null) return;

        RaycastHit hit;
        //to make the raycast work even if you are unarmed, we use a ternary operator to set a new floats value based on whether or not you have a weapon
        float range = Weapon == null ? 30f : Weapon.Range;
        if (Physics.Raycast(_highShootPoint.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            if (hit.distance < 4.57f)
            {
                //AS THE CONCEPT GOES, YOUR CHARACTER WILL THROW A GRENADE IF THE ENEMY IS TOO CLOSE FOR THE UPPER CROSSHAIR DECAL TO DISPLAY ON SCREEN
                //HERE WE ONLY PROJECT THE CROSSHAIR THOUGH, SO ONLY AN INDICATOR THAT THE GRENADE THROW WILL HAPPEN IS NEEDED HERE
                //MAYBE CHANGE THE CROSSHAIR TO RED?
                //OTHERWISE, RED CHEVRONS WHICH POINT UPWARD (MAYBE WITH A GRENADE ICON AS WELL?)
            }

            _highCrosshair.position = hit.point;
            _highCrosshair.rotation = this.transform.rotation;
            _highCrosshair.gameObject.SetActive(true);
            _UiHighCrosshair.SetActive(false);
            return;
        }
        _highCrosshair.gameObject.SetActive(false);
        _UiHighCrosshair.SetActive(true);
    }

    public void OnShootHigh()
    {
        //guard clauses
        if (Weapon == null)
        {
            Debug.Log("no weapon equipped. will not fire");
            return;
        }
        if (Weapon.IsReloading) return;
        
        //after this, go!

        Weapon.Fire(this, 10f, _highShootPoint.position);
        Debug.Log("fired weapon high: " + Weapon.WeaponName + ". " + "Ammo left: " + Weapon.CurrMag);
    }
    public void OnShootLow()
    {
        if (Weapon == null)
        {
            Debug.Log("no weapon equipped. will not fire");
            return;
        }

        if (Weapon.IsReloading) return;


        Weapon.Fire(this, 5f, _lowShootPoint.position);
        Debug.Log("fired weapon low: " + Weapon.WeaponName + ". " + "Ammo left: " + Weapon.CurrMag);
    }

    public void PickUpWeapon(WeaponData weaponToPickup)
    {
        if (Weapon == null)
        {
            Weapon = new Weapon(weaponToPickup);
            return;
        }
        if (weaponToPickup.WeaponName == Weapon.WeaponName)
        {
            //TODO: give player ammo instead of weapon

            return;
        }
        Weapon = new Weapon(weaponToPickup);
    }
}
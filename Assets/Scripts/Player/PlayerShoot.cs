using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Weapon
{
    public Weapon(WeaponData input)
    {
        IsReloading = false;

        WeaponName = input.WeaponName;
        Damage = input.Damage;
        MagCapacity = input.BulletsPerMag;
        AmmoCapacity = input.MaxBulletCapacity;
        _reloadTime = new(input.ReloadTime);
        CurrMag = MagCapacity;
        Range = input.Range;
        _fireRate = new(input.FireRate);
        AmmoStock = input.AmmoStock;
        CurrentWeaponTextListener.Instance.SubscribeToWeapon(this);
    }

    public event EventHandler<WeaponUIEventArgs> AmmoUpdate;

    public bool IsReloading { get; protected set; }
    public string WeaponName { get; private set; }
    public int Damage { get; protected set; }

    //how many bullets are in your current mag
    public int CurrMag { get; protected set; }

    //how many shots you can fire before needing to reload
    public int MagCapacity { get; protected set; }
    private int _ammoStock;
    public int AmmoStock
    {
        get
        {
            return _ammoStock;
        }
        set
        {
            if (value + _ammoStock > AmmoCapacity) _ammoStock = AmmoCapacity;
            else _ammoStock = _ammoStock + value;
        }
    }

    //how many shots you can carry outside of the bullets in each mag
    public int AmmoCapacity { get; private set; }
    public float Range { get; private set; }
    private WaitForSeconds _reloadTime { get; set; }
    private WaitForSeconds _fireRate;

    public virtual IEnumerator Reload()
    {
        //first clause would never happen since this coroutine is only called when currmag != magcapacity,
        //but if i add a reload button in the future itll be useful
        if (CurrMag == MagCapacity || IsReloading || AmmoStock <= 0) yield break;

        Debug.Log("Reloading");
        IsReloading = true;

        //replenish ammo based on how much ammo we have left
        CurrMag = AmmoStock >= MagCapacity ? MagCapacity : AmmoStock;
        //remove how much we reloaded from our stock of ammo
        AmmoStock = -CurrMag;

        yield return _reloadTime;
        IsReloading = false;
        WeaponUIEventArgs args = new(this);
        AmmoUpdate?.Invoke(this, args);
        Debug.Log("DONE reloading. Current ammo stock is: " + AmmoStock);
    }
    public virtual void Fire(Transform player, float radius, Vector3 shootPoint)
    {
        if (CurrMag <= 0 || IsReloading) return;

        CurrMag -= 1;
        WeaponUIEventArgs args = new(this);
        AmmoUpdate?.Invoke(this, args);

        RaycastHit hit;
        if (Physics.Raycast(shootPoint, player.TransformDirection(Vector3.forward), out hit, Range))
        {
            if (hit.transform.TryGetComponent<EnemyHead>(out EnemyHead enemy))
            {
                //Whenever it says "HP = damage" or something of the like, where HP and damage are both ints, it means that the HP will subtract damage from itself
                //this is because HP is a property and its setter runs a method named takedamage which simply subtracts the int damage from a private hp variable
                enemy.HP = Damage;
                return;
            }
        }


    }
    public void UpdateUI(WeaponUIEventArgs args, object sender)
    {
        AmmoUpdate?.Invoke(sender, args);
    }
}


public class PlayerShoot : MonoBehaviour
{
    public event EventHandler<WeaponUIEventArgs> WeaponUIChange;

    protected InputHandler _input;

    protected Transform _highCrosshair;
    [SerializeField] GameObject _uiHighCrosshair;
    [SerializeField] Transform _uiLowCrosshair;
    [SerializeField] Transform _lowCrosshair;

    //weapon variables
    public Weapon CurrWeapon { get; protected set; }
    private Weapon[] _weapons = new Weapon[3];
    private int _currWeaponIndex = 0;

    //these transforms are from where raycasts will fire
    [SerializeField] static Transform _lowShootPoint;
    [SerializeField] static Transform _highShootPoint;


    bool hasInit = false;



    public void InitShoot()
    {
        _input = GetComponent<InputHandler>();
        _lowShootPoint = GameObject.Find("LowShootOrigin").transform;
        _highShootPoint = GameObject.Find("HighShootOrigin").transform;
        _highCrosshair = GameObject.Find("HighCrosshairDecal").transform;
        _lowCrosshair = GameObject.Find("LowCrosshairDecal").transform;
        CurrentWeaponTextListener.Shoots.Add(this);

        if (_highShootPoint == null || _lowShootPoint == null)
        {
            Debug.LogWarning("NOT ENOUGH SHOOTPOINTS ATTACHED, PLEASE ENSURE THAT YOU HAVE INHABITED LOWSHOOTPOINT AND HIGHSHOOTPOINT WITH ANY TRANSFORM(S)");
            Debug.LogWarning("Player will now self destruct in 10...9...");
            this.gameObject.SetActive(false);
        }
        _uiLowCrosshair = GameObject.Find("LOWcrosshair").transform;
        _uiHighCrosshair = GameObject.Find("HIGHcrosshair");

        this.hasInit = true;
    }

    protected void ShootUpdate()
    {
        if (!this.hasInit) return;
        //draw ray for debug purposes
        Debug.DrawRay(_lowShootPoint.position, _lowShootPoint.TransformDirection(Vector3.forward) * 100f, Color.blue);
        Debug.DrawRay(_highShootPoint.position, _highShootPoint.TransformDirection(Vector3.forward) * 100f, Color.green);
        ProjectHighCrossHair();
        ProjectLowCrossHair();
        PollForInput();

        //handle things that are weapon-related after this guard clause
        if (CurrWeapon == null) return;
        if (CurrWeapon.CurrMag <= 0) StartCoroutine(CurrWeapon.Reload());
    }


    protected virtual void ProjectHighCrossHair()
    {
        //if (_highCrosshair == null) return;
        RaycastHit hit;
        //to make the raycast work even if you are unarmed, we use the ternary operator to set a new floats value based on whether or not you have a weapon
        float range = CurrWeapon == null ? 30f : CurrWeapon.Range;
        if (Physics.Raycast(_highShootPoint.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            if (hit.distance < 4.57f) //4.57f is how close the enemy is when the crosshair starts go off-screen
            {
                //AS THE CONCEPT GOES, YOUR CHARACTER WILL THROW A GRENADE IF THE ENEMY IS TOO CLOSE FOR THE UPPER CROSSHAIR DECAL TO DISPLAY ON SCREEN
                //HERE WE ONLY PROJECT THE CROSSHAIR THOUGH, SO ONLY AN INDICATOR THAT THE GRENADE THROW WILL HAPPEN IS NEEDED HERE
                //MAYBE CHANGE THE CROSSHAIR TO RED?
                //OTHERWISE, RED CHEVRONS WHICH POINT UPWARD (MAYBE WITH A GRENADE ICON AS WELL?)
            }
            _highCrosshair.position = hit.point;
            _highCrosshair.rotation = this.transform.rotation;
            _highCrosshair.gameObject.SetActive(true);
            _uiHighCrosshair.SetActive(false);
            return;
        }
        _highCrosshair.gameObject.SetActive(false);
        _uiHighCrosshair.SetActive(true);
    }
    protected virtual void ProjectLowCrossHair()
    {
        //if (_highCrosshair == null) return;
        RaycastHit hit;
        //to make the raycast work even if you are unarmed, we use the ternary operator to set a new floats value based on whether or not you have a weapon
        float range = CurrWeapon == null ? 30f : CurrWeapon.Range;
        if (Physics.Raycast(_lowShootPoint.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            _lowCrosshair.position = hit.point;
            _lowCrosshair.rotation = this.transform.rotation;
            _lowCrosshair.gameObject.SetActive(true);
            _uiLowCrosshair.gameObject.SetActive(false);
            return;
        }
        _lowCrosshair.gameObject.SetActive(false);
        _uiLowCrosshair.gameObject.SetActive(true);
    }

    public virtual void Special()
    {
        Debug.Log("special used");
    }
    protected void PollForInput()
    {
        if (_input.Special.WasPressedThisFrame()) Special();
        if (_input.ShootHigh.WasPressedThisFrame())
        {
            if (CurrWeapon == null) return;
            if (CurrWeapon.IsReloading) return;
            CurrWeapon.Fire(this.transform, 10f, _highShootPoint.position);
            WeaponUIEventArgs args = new(CurrWeapon);
            args.IsSimple = true;
            WeaponUIChange?.Invoke(this, args);
            //im hoping this return only keeps the player from shooting both high and low on the same frame
            return;
        }
        if (_input.ShootLow.WasPressedThisFrame())
        {
            if (CurrWeapon == null) return;
            if (CurrWeapon.IsReloading) return;
            CurrWeapon.Fire(this.transform, 10f, _lowShootPoint.position);
            WeaponUIEventArgs args = new(CurrWeapon);
            args.IsSimple = true;
            WeaponUIChange?.Invoke(this, args);
        }
        if (_input.NextWeapon.WasPressedThisFrame())
        {
            if (_currWeaponIndex >= _weapons.Length - 1) return;
            //Make sure the player can only access one "unarmed" weapon slot at a time
            if(CurrWeapon == null && _weapons[_currWeaponIndex + 1] == null) return;

            WeaponUIEventArgs args = new(_weapons[++_currWeaponIndex]);
            WeaponUIChange?.Invoke(this, args);
            CurrWeapon = _weapons[_currWeaponIndex];

        }
        if (_input.PrevWeapon.WasPressedThisFrame())
        {
            if(_currWeaponIndex <= 0) return;
            if(CurrWeapon == null && _weapons[_currWeaponIndex - 1] == null) return;
            
            
            WeaponUIEventArgs args = new(_weapons[--_currWeaponIndex]);
            WeaponUIChange?.Invoke(this, args);
            CurrWeapon = _weapons[_currWeaponIndex];
        }
    }
    public void PickUpWeapon(Weapon weaponToPickup)
    {
        for (int i = 0; i < _weapons.Length - 1; i++)
        {
            if (weaponToPickup == _weapons[i])
            {
                //weapons have a special setter for ammostock which makes sure they stay within the limits of their max ammo capacity
                CurrWeapon.AmmoStock = _weapons[i].AmmoStock;
                return;
            }
            if (_weapons[i] == null)
            {
                _weapons[i] = weaponToPickup;
                CurrWeapon = _weapons[i];
                WeaponUIEventArgs args = new(weaponToPickup);
                WeaponUIChange.Invoke(this, args);
                return;
            }
        }
    }
}
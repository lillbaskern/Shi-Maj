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

        ShootSound = input.ShootSound;
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

    public AudioClip ShootSound { get; private set; }


    //Weapon state variables
    public bool IsReloading { get; protected set; }
    public int CurrMag { get; protected set; }
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
    private int _ammoStock;
    private bool _isReadyToFire = true;



    //variables which could almost be const if i just knew exactly what that would mean for the game
    public string WeaponName { get; private set; }
    public int Damage { get; protected set; }
    public int MagCapacity { get; protected set; }
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
    }
    public virtual IEnumerator Fire(Transform cameraTransform, float radius, AudioSource source)
    {
        if (CurrMag <= 0 || IsReloading) yield break;
        if (!_isReadyToFire) yield break;
        _isReadyToFire = false;

        source.Play();
        CurrMag -= 1;
        WeaponUIEventArgs args = new(this);
        AmmoUpdate?.Invoke(this, args);

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Range))
        {
            Debug.Log(hit.point);
            if (hit.transform.TryGetComponent<IShootable>(out IShootable hitTarget))
            {
                hitTarget.Hit(Damage);
            }
        }
        yield return _fireRate;
        _isReadyToFire = true;
    }
    public void UpdateUI(WeaponUIEventArgs args, object sender)
    {
        AmmoUpdate?.Invoke(sender, args);
    }
}

[DefaultExecutionOrder(-1)]
public class PlayerShoot : MonoBehaviour
{
    AudioSource _audioSource;
    public event EventHandler<WeaponUIEventArgs> WeaponUIChange;

    protected InputHandler _input;

    protected Transform _highCrosshair;
    [SerializeField] GameObject _uiHighCrosshair;
    [SerializeField] Transform _uiLowCrosshair;
    [SerializeField] Transform _lowCrosshair;
    protected CharacterController _cc;

    //weapon variables
    public Weapon CurrWeapon { get; protected set; }
    private Weapon[] _weapons = new Weapon[3];
    private int _currWeaponIndex = 0;

    //these transforms are from where raycasts will fire
    [SerializeField] static Transform _lowShootPoint;
    [SerializeField] static Transform _highShootPoint;

    protected Transform _cameraTransform;


    bool hasInit = false;



    public void InitShoot()
    {
        _audioSource = GetComponent<AudioSource>();
        _input = GetComponent<InputHandler>();
        _cameraTransform = Camera.main.transform;


        CurrentWeaponTextListener.Shoots.Add(this);

        this.hasInit = true;
    }

    protected void ShootUpdate()
    {
        if (!this.hasInit) return;

        
        PollForInput();

        //handle things that are weapon-related after this guard clause
        if (CurrWeapon == null) return;
        if (CurrWeapon.CurrMag <= 0) StartCoroutine(CurrWeapon.Reload());
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
            StartCoroutine(CurrWeapon.Fire(_cameraTransform, 10f, _audioSource));
            WeaponUIEventArgs args = new(CurrWeapon);
            args.IsSimple = true;
            WeaponUIChange?.Invoke(this, args);
            return;
        }
        if (_input.NextWeapon.WasPressedThisFrame())
        {
            //todo:: maybe make weapon swapping loop from top of list to bottom and vice versa
            if (_currWeaponIndex >= _weapons.Length - 1) return;
            //Make sure the player can only access one "unarmed" weapon slot at a time
            if (CurrWeapon == null && _weapons[_currWeaponIndex + 1] == null) return;

            WeaponUIEventArgs args = new(_weapons[++_currWeaponIndex]);
            WeaponUIChange?.Invoke(this, args);

            CurrWeapon = _weapons[_currWeaponIndex];
            //set audio clip to current weapons shootsound
            _audioSource.clip = CurrWeapon?.ShootSound;

        }
        if (_input.PrevWeapon.WasPressedThisFrame())
        {
            if (_currWeaponIndex <= 0) return;
            if (CurrWeapon == null && _weapons[_currWeaponIndex - 1] == null) return;


            WeaponUIEventArgs args = new(_weapons[--_currWeaponIndex]);
            WeaponUIChange?.Invoke(this, args);
            CurrWeapon = _weapons[_currWeaponIndex];
            //set audio clip to current weapons shootsound
            _audioSource.clip = CurrWeapon?.ShootSound;

        }
    }
    public void PickUpWeapon(Weapon weaponToPickup)
    {

        UITextPromptArgs promptArgs = new($"Picking up {weaponToPickup.WeaponName}");
        UITextPromptObserver.SendUITextPrompt(this, promptArgs);


        //first, loop through each weapon with each weapon and see if any weapon is identical to the one being picked up
        for (int i = 0; i < _weapons.Length - 1; i++)
        {
            for (int j = 0; j < _weapons.Length - 1; j++)
            {
                if (_weapons[i] != null && _weapons[i] == _weapons[j])
                {
                    _weapons[i].AmmoStock += weaponToPickup.AmmoStock;

                    WeaponUIEventArgs args = new(_weapons[i]);
                    args.IsSimple = true;
                    WeaponUIChange?.Invoke(this,args);

                    return;
                }
            }
        }

        for (int i = 0; i < _weapons.Length - 1; i++)
        {
            if (_weapons[i] == null)
            {
                _weapons[i] = weaponToPickup;
                CurrWeapon = _weapons[i];
                _audioSource.clip = CurrWeapon?.ShootSound;

                //invoke weapon ui event
                WeaponUIEventArgs weaponUIArgs = new(weaponToPickup);
                WeaponUIChange?.Invoke(this, weaponUIArgs);
                return;//not necessary but its my little baby
            }
        }
    }
}
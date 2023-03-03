using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : Weapon
{
    public Pistol(WeaponData input) : base(input)
    {
    }
}
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
    }

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
            if(value + _ammoStock > AmmoCapacity) _ammoStock = AmmoCapacity;
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
        Debug.Log("DONE reloading. Current ammo stock is: " + AmmoStock);
    }
    //Base fire method for any type of weapon which attacks once per input (doesnt even have to be just a gun)
    public virtual void Fire(Transform player, float radius, Vector3 shootPoint)
    {
        if (CurrMag == 0 || IsReloading) return;
        CurrMag -=1;
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
        Debug.Log("fired weapon: " + WeaponName + ". Ammo left in mag: " + CurrMag);

    }
}

public class PlayerShoot : MonoBehaviour
{
    protected InputHandler _input;
    static Transform _highCrosshair;
    [SerializeField] static GameObject _UiHighCrosshair;
    public Weapon Weapon { get; protected set; }

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

        if (_highShootPoint == null || _lowShootPoint == null)
        {
            Debug.LogWarning("NOT ENOUGH SHOOTPOINTS ATTACHED, PLEASE ENSURE THAT YOU HAVE INHABITED LOWSHOOTPOINT AND HIGHSHOOTPOINT WITH ANY TRANSFORM(S)");
            Debug.LogWarning("Player will now self destruct in 10...9...");
            this.gameObject.SetActive(false);
        }

        _UiHighCrosshair = GameObject.Find("HIGHcrosshair");
        this.hasInit = true;
    }

    protected void ShootUpdate()
    {
        if (!this.hasInit) return;
        //draw ray for debug purposes
        Debug.DrawRay(_lowShootPoint.position, _lowShootPoint.TransformDirection(Vector3.forward) * 100f, Color.blue);
        Debug.DrawRay(_highShootPoint.position, _highShootPoint.TransformDirection(Vector3.forward) * 100f, Color.green);
        ProjectHighCrossHair();
        PollForInput();

        //handle things that are weapon-related after this guard clause
        if (Weapon == null) return;
        if (Weapon.CurrMag <= 0) StartCoroutine(Weapon.Reload());
    }


    protected virtual void ProjectHighCrossHair()
    {
        //if (_highCrosshair == null) return;
        RaycastHit hit;
        //to make the raycast work even if you are unarmed, we use the ternary operator to set a new floats value based on whether or not you have a weapon
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

    protected virtual void PollForInput()
    {

        if (_input.ShootHigh.WasPressedThisFrame())
        {
            Debug.Log(Weapon);
            Weapon?.Fire(this.transform, 10f, _highShootPoint.position);
            //im hoping this return only keeps the player from shooting both high and low on the same frame
            return;
        }
        if (_input.ShootLow.WasPressedThisFrame())
        {
            Weapon?.Fire(this.transform, 10f, _lowShootPoint.position);
        }
    }

}
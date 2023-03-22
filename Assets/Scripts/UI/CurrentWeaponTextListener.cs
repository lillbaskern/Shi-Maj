using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentWeaponTextListener : MonoBehaviour
{

    public static CurrentWeaponTextListener Instance { get; private set; }
    [SerializeField] TextMeshProUGUI _textMeshAmmo;
    [SerializeField] TextMeshProUGUI _textMeshWeapon;
    public static List<PlayerShoot> Shoots = new();
    public static PlayerHead PlayerHead;
    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }
    public void SubscribeToWeapon(Weapon weapon)
    {
        weapon.AmmoUpdate += OnWeaponUiChange;
    }
    IEnumerator Start()
    {
        //wait for list Shoots to be filled
        yield return new WaitForSeconds(0.1f);//this is unnecessary cus u can customize the execution order in your project settings
        foreach (PlayerShoot shoot in Shoots) shoot.WeaponUIChange += OnWeaponUiChange;
        
        PlayerHead.UpdateWeaponNameUIUnarmed += OnUpdateWeaponUIUnarmed;

        //you start off as unarmed we'll set the text manually here
        _textMeshAmmo.text = "";
        _textMeshWeapon.text = "Unarmed";
    }
    void OnWeaponUiChange(object sender, WeaponUIEventArgs args)
    {
        if (args.WeaponName != null) _textMeshWeapon.text = args.WeaponName;
        if (args.AmmoCache == -1)
        {
            _textMeshAmmo.text = "";
            return;
        }
        _textMeshAmmo.text = $"{args.currMag} | {args.AmmoCache}";
    }
    void OnUpdateWeaponUIUnarmed()
    {
        _textMeshAmmo.text = "";
        _textMeshWeapon.text = "Unarmed";
    }

    private void OnDisable()
    {
        foreach (PlayerShoot shoot in Shoots)
        {
            shoot.WeaponUIChange -= OnWeaponUiChange;
        }
    }
}

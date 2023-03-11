using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentWeaponListener : MonoBehaviour
{

    public static CurrentWeaponListener Instance { get; private set; }
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
        yield return new WaitForSeconds(0.1f);
        foreach (PlayerShoot shoot in Shoots)
        {
            shoot.WeaponUIChange += OnWeaponUiChange;
        }
        PlayerHead.UpdateWeaponNameUIUnarmed += OnUpdateWeaponUIUnarmed;
        _textMeshAmmo.text = "";
        _textMeshWeapon.text = "Unarmed";
    }
    void OnWeaponUiChange(object sender, WeaponUIEventArgs args)
    {
        if (args.WeaponName != null) _textMeshWeapon.text = args.WeaponName;
        _textMeshAmmo.text = $"{args.currMag} | {args.AmmoCache}";
    }
    void OnUpdateWeaponUIUnarmed()
    {
        _textMeshAmmo.text = "";
        _textMeshWeapon.text = "Unarmed";
    }
}

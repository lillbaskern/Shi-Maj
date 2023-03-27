using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModelWeaponChangeListener : MonoBehaviour
{
    [SerializeField]GameObject unarmed;
    [SerializeField]GameObject pistol;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.13f);//i will learn the execution order next week, sorry.
        foreach (PlayerShoot shoot in CurrentWeaponTextListener.Shoots) shoot.WeaponUIChange += OnUIChange;
        unarmed = GameObject.Find("UnarmedViewmodel");
        pistol = GameObject.Find("GunViewmodel");
    }

    //this shit is horrible
    void OnUIChange(object sender, WeaponUIEventArgs args)
    {
        if (args.WeaponName == "Pistol")
        {
            unarmed.SetActive(false);
            pistol.SetActive(true);
        }
        if (args.WeaponName == "Unarmed")
        {
            pistol.SetActive(false);
            unarmed.SetActive(true);
        }
    }
}

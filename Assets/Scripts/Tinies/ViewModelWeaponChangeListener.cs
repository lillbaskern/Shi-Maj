using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModelWeaponChangeListener : MonoBehaviour
{
    [SerializeField]GameObject unarmed;
    [SerializeField]GameObject pistol;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.11f);
        foreach (PlayerShoot shoot in CurrentWeaponTextListener.Shoots) shoot.WeaponUIChange += OnUIChange;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModelAnimWeapChangeListener : MonoBehaviour
{
    Animator _anim;
    IEnumerator Start()
    {
        _anim = GetComponent<Animator>();
        yield return new WaitForSeconds(0.11f);
        foreach (PlayerShoot shoot in CurrentWeaponTextListener.Shoots) shoot.WeaponUIChange += OnWeapChange;
    }

    void OnWeapChange(object sender, WeaponUIEventArgs args)
    {
        if(args.IsSimple) return;
        _anim.Play("Equip");
    }
}

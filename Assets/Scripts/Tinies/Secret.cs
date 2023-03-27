using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret : MonoBehaviour, IShootable
{
    public void Hit(int Damage)
    {
        Debug.Log("bruh");
        this.GetComponent<Animator>().Play("OpenSecretDoor");
    }
}

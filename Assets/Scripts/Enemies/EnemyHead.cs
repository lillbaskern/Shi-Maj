using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour, IShootable
{
    [SerializeField] private int _hp = 4;

    public void Hit(int Damage)
    {
        _hp -= Damage;
        if (_hp <= 0) 
        {
            PlayerHead.Instance.AddGold(50);
            Destroy(this.gameObject); 
        }
    }
}

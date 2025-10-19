using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyHead : MonoBehaviour, IShootable
{
    [SerializeField] private int _hp = 4;
    [SerializeField] private int _goldYield = 50;
    [SerializeField] private int _xpToDrop = 20;
    [SerializeField] private GameObject _xpSphere;
    public GameObject Drop;
    public void Hit(int Damage)
    {
        _hp -= Damage;
        if (_hp <= 0)
        {
            PlayerHead.Instance.AddGold(_goldYield);

            Instantiate(_xpSphere, this.transform.position, Quaternion.identity);
            if(Drop) Instantiate(Drop, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject); 
        }
    }
}

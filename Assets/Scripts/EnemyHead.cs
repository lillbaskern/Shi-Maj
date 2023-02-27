using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private int _hp = 4;
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = TakeDamage(_hp, value);
            Debug.Log(this.gameObject.name + " took damage! Remaining hp: " + _hp);
        }
    }

    int TakeDamage(int currHP, int incomingDamage)
    {
        return currHP - incomingDamage;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

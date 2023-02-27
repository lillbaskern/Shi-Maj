using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowEnemyProto : MonoBehaviour
{
    GameObject _player;
    public int Damage = 3; 
    [SerializeField]float MoveSpeed = 3;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.TryGetComponent<PlayerHead>(out PlayerHead head)) head.TakeDamage(Damage);
    }
}

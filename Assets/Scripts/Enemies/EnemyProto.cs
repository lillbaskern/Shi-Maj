using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Damage;
    public int HP;
    public float MoveSpeed;
}

public class EnemyProto : MonoBehaviour
{

    public int Damage = 3;
    //if the enemy collides with player, damage player
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<PlayerHead>(out PlayerHead head)) head.HP = Damage;
    }
}

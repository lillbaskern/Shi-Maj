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
    public int HP = 8;
    public float Range;
    public float FireCooldown;
    public Transform Player;
    public GameObject ProjectilePrefab;

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, Player.position) <= Range && (int)(Time.time % FireCooldown) == 0)
        {
            var projectile = Instantiate(ProjectilePrefab);
            
        }
    }
}

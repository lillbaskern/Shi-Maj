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

public class EnemyProto : MonoBehaviour, IShootable
{
    public int HP = 8;
    public float Range;
    public float FireCooldown;
    public Transform Player;
    public GameObject ProjectilePrefab;
    float _fireCoolDown;

    public void Hit(int Damage)
    {
        HP -= Damage;
        if (HP <= 0) Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        //Debug.Log((int)(Time.time % FireCooldown) == 0);
        if (Vector3.Distance(this.transform.position, Player.position) <= Range && (int)(Time.time % FireCooldown) == 0 && _fireCoolDown >= FireCooldown)
        {
            //todo: add raycast to make sure nothing is inbetween player and enemy
            Fire();
            _fireCoolDown = 0;
        }
        else _fireCoolDown += Time.deltaTime;

    }

    private void Fire()
    {
        var projectile = Instantiate(ProjectilePrefab, this.transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileBehaviour>().InitProj(Player);
    }
}

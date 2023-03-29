using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public int Damage = 13;
    public Transform Target = null;
    public float MoveSpeed;
    
    //initproj is called from whichever enemy fired the projectile immediately after instantiation
    public void InitProj(Transform target)
    {
        Target = target;
        transform.LookAt(Target);
        Destroy(this.gameObject,5f);
    }

    void FixedUpdate()
    {
        if (Target == null) return;
        this.transform.Translate(Vector3.forward * MoveSpeed, Space.Self);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHead>().HP = Damage;
        }
        Destroy(this.gameObject);
    }
}

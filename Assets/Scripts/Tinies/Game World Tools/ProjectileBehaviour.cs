using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public int Damage = 13;
    public Transform Target = null;
    public float MoveSpeed;
    public void InitProj(Transform target)
    {
        Target = target;
        transform.LookAt(Target);
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
            //make player take damage, play sound, change ui. whatever
            other.GetComponent<PlayerHead>().HP = Damage;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

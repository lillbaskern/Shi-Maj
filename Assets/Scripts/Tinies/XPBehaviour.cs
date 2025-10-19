using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBehaviour : MonoBehaviour
{
    [SerializeField] float _maxDistance = 40f; //max distance for the xp sphere to fly on spawn
    public float XPYield = 50f;
    Rigidbody _rb;


    void Start()
    {
        Vector3 dir = Random.insideUnitSphere;
        dir.Normalize();
        dir.y = 4;
        float force = Random.Range(45f, _maxDistance);
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(dir * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHead>();
        if (player)
        {
            player.AddXP(XPYield);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: make sure this actually does what its supposed to (give the player more hp)
            other.GetComponent<PlayerHead>().HP = -10;
        }
    }
}

using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    void Start()
    {
        InvokeRepeating("Spawn", 4f, 7f);
    }


    void Spawn()
    {
        int random = Random.Range(1,3);

        if (random == 1)
        {
            Instantiate(EnemyToSpawn, this.transform.position, Quaternion.identity);
        }
    }
}

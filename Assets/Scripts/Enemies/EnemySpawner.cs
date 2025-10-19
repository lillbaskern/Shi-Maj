using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void SendToGameManager();
    public void EnemyFixedUpdate();
}

public class EnemySpawner : MonoBehaviour
{
    public bool SpawnOnce;
    public int EnemiesToSpawn;


    bool _firstSpawn;
    public GameObject EnemyToSpawn;

    public float Radius = 5f;
    void Start()
    {
        if (SpawnOnce)
        {



            for (int i = 0; i < EnemiesToSpawn; i++) 
            {

                Vector3 randomDirection = Random.insideUnitSphere;
                randomDirection.y = 0;
                randomDirection.Normalize();
                float randomDistance = Random.Range(0f, Radius);


                Vector3 spawnPoint = transform.position + randomDirection * randomDistance;
                Instantiate(EnemyToSpawn, spawnPoint, Quaternion.identity);
            }

            return;
        }



        _firstSpawn = true;
        int randomSpawnTime = Random.Range(5, 11);
        InvokeRepeating("Spawn", randomSpawnTime, 3f);
    }


    void Spawn()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();
        float randomDistance = Random.Range(0f, Radius);


        Vector3 spawnPoint = transform.position + randomDirection * randomDistance;

        
        if (_firstSpawn)
        {
            _firstSpawn = false;
            Instantiate(EnemyToSpawn, spawnPoint, Quaternion.identity);
            return;
        }


        int random = Random.Range(1,5);

        if (random == 1)
        {
            Instantiate(EnemyToSpawn, spawnPoint, Quaternion.identity);
        }
    }
}

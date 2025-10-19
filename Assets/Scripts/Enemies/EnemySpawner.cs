using AYellowpaper.SerializedCollections;
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
    public float SpawnRate = 1;
    public bool SpawnOnce;
    public int EnemiesToSpawn;

    public SimpleDropTableProto DropTable;

    int _spawnedEnemies = 0;
    public int MaxEnemies = 100;

    bool _firstSpawn;
    public GameObject EnemyToSpawn;

    public float Radius = 5f;
    void Start()
    {
        if (SpawnOnce)
        {



            for (int i = 0; i < EnemiesToSpawn; i++) 
            {
                _spawnedEnemies++;
                Vector3 randomDirection = Random.insideUnitSphere;
                randomDirection.y = 0;
                randomDirection.Normalize();
                float randomDistance = Random.Range(0f, Radius);




                Vector3 spawnPoint = transform.position + randomDirection * randomDistance;
                
                EnemyHead head = Instantiate(EnemyToSpawn, spawnPoint, Quaternion.identity).GetComponent<EnemyHead>();

                
                foreach(var entry in DropTable.Table)
                {
                    int random = Random.Range(1, 101);
                    if(random <= entry.Value)
                    {
                        head.Drop = entry.Key;
                        break;
                    }
                }
            }

            return;
        }



        _firstSpawn = true;
        int randomSpawnTime = Random.Range(3, 11);
        Invoke("Spawn", randomSpawnTime);
    }


    void Spawn()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();
        float randomDistance = Random.Range(0f, Radius);


        Vector3 spawnPoint = transform.position + randomDirection * randomDistance;


        Invoke("Spawn", 3f * SpawnRate);

        if (_firstSpawn)
        {
            _firstSpawn = false;
            EnemyHead head = Instantiate(EnemyToSpawn, spawnPoint, Quaternion.identity).GetComponent<EnemyHead>();
            foreach (var entry in DropTable.Table)
            {
                int rand = Random.Range(1, 101);
                if (rand <= 2)
                {
                    head.Drop = entry.Key;
                    break;
                }
            }
            _spawnedEnemies++;
            return;
        }


        int random = Random.Range(1,5);

        if (random == 1)
        {
            EnemyHead head = Instantiate(EnemyToSpawn, spawnPoint, Quaternion.identity).GetComponent<EnemyHead>();
            foreach (var entry in DropTable.Table)
            {
                int rand = Random.Range(1, 101);
                if (rand <= entry.Value)
                {
                    head.Drop = entry.Key;
                    break;
                }
            }
            _spawnedEnemies++;
        }
    }
}

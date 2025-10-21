using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform PlayerTransform;
    public float GameStartTime;

    public List<IEnemy> Enemies;

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        if (PlayerTransform == null) return;

        Enemies = new List<IEnemy>();

        GameStartTime = Time.realtimeSinceStartup;
    }

    private void FixedUpdate()
    {
        if(Enemies == null || Enemies.Count == 0) { return; }
        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].EnemyFixedUpdate();
        }
    }
}

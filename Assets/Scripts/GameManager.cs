using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform PlayerTransform;
    public float GameStartTime;

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

            GameStartTime = Time.realtimeSinceStartup;
        PlayerTransform = GameObject.Find("Player").transform;
    }




}

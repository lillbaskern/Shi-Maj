using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    
    public Transform Target;
    public float MoveSpeed;
    void Start()
    {
        transform.LookAt(Target);
    }

    void Update()
    {

    }
}

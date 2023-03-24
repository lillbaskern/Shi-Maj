using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GeneralTriggerEvent : MonoBehaviour
{
    public UnityEvent OnTriggerEntered;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) OnTriggerEntered.Invoke();
    }
}

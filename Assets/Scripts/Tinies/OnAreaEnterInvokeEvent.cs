using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAreaEnterInvokeEvent : MonoBehaviour
{
    public UnityEvent OnTriggerEntered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEntered?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] string _promptText = "Picked up 10HP booster";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //why did i change the setter, it is not helping with readability
            other.GetComponent<PlayerHead>().HP = -10;
            
            UITextPromptArgs args = new(_promptText);
            UITextPromptObserver.SendUITextPrompt(this, args);

            Destroy(this.gameObject);
        }
    }
}

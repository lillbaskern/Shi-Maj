using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfAndroid : MonoBehaviour
{
    private void Awake()
    {
        if(Application.platform == RuntimePlatform.Android) this.gameObject.SetActive(false);
    }
}

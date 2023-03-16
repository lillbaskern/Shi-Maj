using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugStickValue : MonoBehaviour
{
    public TextMeshProUGUI textmesh;

    void Update()
    {
        textmesh.text = InputHandler.Instance.Move.ReadValue<Vector2>().ToString();
    }
}

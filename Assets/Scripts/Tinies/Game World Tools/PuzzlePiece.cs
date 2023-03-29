using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[DefaultExecutionOrder(1)]
public class PuzzlePiece : MonoBehaviour, IShootable
{
    public UnityEvent PuzzleDone;
    Material _material;
    public bool BeenTriggered;
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = Color.blue;
        PuzzleManager.AddPiece(this);
    }

    public void Hit(int Damage)
    {
        if(BeenTriggered) return;
        BeenTriggered = true;
        _material.color = Color.green;
        PuzzleManager.CheckPuzzleState();
    }
}

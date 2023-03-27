using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IShootable
{
    public void Hit(int Damage);
}

public class PuzzleManager : MonoBehaviour
{
    public static List<PuzzlePiece> PuzzlePieces = new();

    public static Animator DoorAnimator;

    private void Start()
    {
        DoorAnimator = GameObject.Find("Door").GetComponent<Animator>();
        CheckPuzzleState();
    }
    public static void AddPiece(PuzzlePiece piece)
    {
        Debug.Log("adding piece: " + piece.name);
        PuzzlePieces.Add(piece);
    }

    public static void CheckPuzzleState()
    {
        bool PuzzleDone = false;
        int debugInt = 0;
        
        foreach (PuzzlePiece piece in PuzzlePieces)
        {
            if(piece.BeenTriggered) debugInt++;
        }
        PuzzleDone = debugInt > 2;

        if(PuzzleDone)DoorAnimator?.Play("DoorOpen");
    }
}

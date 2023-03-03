using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LifeLongWrestler : PlayerMove
{
    InputHandler _input;
    void Start()
    {
        _input = gameObject.AddComponent<InputHandler>();
    }

    void Update()
    {
        MoveAndTurnLoop(_input.Turn,_input.Move);
    }
}

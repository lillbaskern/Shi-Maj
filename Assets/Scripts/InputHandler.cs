using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    //movement
    private InputAction _move;
    private InputAction _turn;

    //weapon usage
    private InputAction _shootHigh;
    private InputAction _shootLow;

    //misc
    private InputAction _special;
    private InputAction _interact;


    public InputAction Move
    {
        get
        {
            return _move;
        }
    }
    public InputAction Interact
    {
        get
        {
            return _interact;
        }
    }
    public InputAction ShootLow
    {
        get
        {
            return _shootLow;
        }
    }
    public InputAction ShootHigh
    {
        get
        {
            return _shootHigh;
        }
    }
    public InputAction Special
    {
        get
        {
            return _special;
        }
    }
    public InputAction Turn
    {
        get
        {
            return _turn;
        }
    }



    private Controls _controls;
    public Controls Controls
    {
        //this allows us to set controls at runtime
        set
        {
            _controls = value;
            SetInputActions();
        }
        get
        {
            return _controls;
        }
    }

    private void SetInputActions()
    {
        _move = _controls.Player.Move;
        _move.Enable();

        _turn = _controls.Player.Turn;
        _turn.Enable();

        _shootHigh = _controls.Player.ShootHigh;
        _shootHigh.Enable();

        _shootLow = _controls.Player.ShootLow;
        _shootLow.Enable();

        _interact = _controls.Player.Interact;
        _interact.Enable();

        _special = _controls.Player.Special;
        _special.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
        _turn.Disable();
        _shootHigh.Disable();
        _shootLow.Disable();
        _interact.Disable();
        _special.Disable();
    }
}

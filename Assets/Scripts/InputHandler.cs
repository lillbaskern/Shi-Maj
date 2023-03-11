using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //this is a one player game, so this is ok
    public static InputHandler Instance { get; private set; }

    //movement
    private InputAction _move;
    private InputAction _turn;
    private InputAction _jump;

    //weapon usage
    private InputAction _shootHigh;
    private InputAction _shootLow;

    //character switching
    private InputAction _nextChar;
    private InputAction _prevChar;

    //misc
    private InputAction _special;
    private InputAction _interact;


    //movement
    public InputAction Move
    {
        get
        {
            return _move;
        }
    }
    public InputAction Turn
    {
        get
        {
            return _turn;
        }
    }
    public InputAction Jump
    {
        get
        {
            return _jump;
        }
    }
    //weapon usage
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

    //character switching
    public InputAction NextChar
    {
        get { return _nextChar; }
    }
    public InputAction PrevChar
    {
        get { return _prevChar; }
    }

    //misc
    public InputAction Interact
    {
        get
        {
            return _interact;
        }
    }

    public InputAction Special
    {
        get
        {
            return _special;
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

    private void Start()
    {
        //singleton declaration
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //do this so setinputactions is called
        Controls = new();

    }

    public void SetInputActions()
    {
        Debug.Log("setinputactions called");
        _nextChar = _controls.Player.NextChar;
        _nextChar.Enable();
        
        _prevChar = _controls.Player.PrevChar;
        _prevChar.Enable();

        _move = _controls.Player.Move;
        _move.Enable();

        _turn = _controls.Player.Turn;
        _turn.Enable();

        _jump = _controls.Player.Jump;
        _jump.Enable();

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
        _nextChar.Disable();
        _prevChar.Disable();
        _move.Disable();
        _turn.Disable();
        _jump.Disable();
        _shootHigh.Disable();
        _shootLow.Disable();
        _interact.Disable();
        _special.Disable();
    }
}

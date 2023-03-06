using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//<SUMMARY>
//CLASS WHICH HANDLES PLAYER MOVEMENT
//IS INHERITED BY ANY CHARACTER
public class PlayerMove : PlayerShoot
{
    Camera _cam;
    //keeping this public for now. i doubt any gameplay method would even get close to changing this variable anyways
    public float LookSensitivity = 4;
    CharacterController _cc;



    [SerializeField] protected float _jumpHeight = 100;
    [SerializeField] protected float _fallSpeed = 0.5f;
    [SerializeField] protected float _moveSpeed = 5f;

    float _moveLerp = 0f;

    [SerializeField] Vector2 _inputDir;
    private Vector2 _turnDir;
    [SerializeField, Tooltip("How fast the player accelerates"), Range(0.1f, 50f)] float _moveAccel = 10f;

    Vector3 _moveDir;
    private float _verticalVel;


    public bool IsGrounded { get; private set; }

    void Awake() => _cc = GetComponent<CharacterController>();

    protected void SendToCharList(ICharacter character)
    {
        if (PlayerHead.Characters.Contains(character)) return;
        PlayerHead.Characters.Add(character);
    }

    protected void MoveAndTurnLoop(InputAction turn, InputAction move, InputAction Jump)
    {
        //read input vectors
        _turnDir = turn.ReadValue<Vector2>();
        _inputDir = move.ReadValue<Vector2>();

        //Set isgrounded
        IsGrounded = _cc.isGrounded;
        if (IsGrounded) _verticalVel = 0;

        //jumping and gravity (prototype only)
        if (Jump.WasPressedThisFrame() && IsGrounded)
        {
            this._verticalVel += Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }
        //you can further dissolve Physics.gravity.y into float _MaxFallSpeed 
        _verticalVel += Physics.gravity.y * Time.deltaTime;
        _verticalVel = Mathf.Clamp(_verticalVel,Physics.gravity.y,50f);


        _cc.transform.Rotate(Vector3.up * _turnDir.x * (Time.deltaTime * 100));

        //create final move vector
        _moveDir = new Vector3(_inputDir.x * _moveSpeed, _verticalVel, _inputDir.y * _moveSpeed);
        _cc.Move(_cc.transform.rotation * _moveDir * Time.deltaTime);
    }
}

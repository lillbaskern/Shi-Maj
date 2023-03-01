using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//<SUMMARY>
//CLASS WHICH HANDLES PLAYER MOVEMENT
//IS INHERITED BY ANY CHARACTER
public class PlayerMove : MonoBehaviour
{
    Camera _cam;
    //keeping this public for now. i doubt any gameplay method would even get close to changing this variable anyways
    public float LookSensitivity = 4;
    CharacterController _cc;



    [SerializeField] protected float _jumpHeight = 1;
    [SerializeField] protected float _fallSpeed = 0.5f;
    [SerializeField] protected float _moveSpeed = 5f;


    [SerializeField] Vector2 _inputDir;
    private Vector2 _turnDir;

    private float _verticalVel;

    public bool IsGrounded { get; private set; }

    void Awake() => _cc = GetComponent<CharacterController>();

    protected void MoveAndTurnLoop()
    {
        //Set isgrounded
        IsGrounded = _cc.isGrounded;
        if (IsGrounded) _verticalVel = 0;
        
        Vector3 _moveDir = new Vector3(_inputDir.x * _moveSpeed, _verticalVel, _inputDir.y * _moveSpeed);
        
        //gravity
        _verticalVel = Mathf.MoveTowards(_verticalVel, Physics.gravity.y, _fallSpeed);

        
        _cc.transform.Rotate(Vector3.up * _turnDir.x * (Time.deltaTime * 100));
        _cc.Move(_cc.transform.rotation * _moveDir * Time.deltaTime);
    }

    public void OnMove(InputValue val) => _inputDir = val.Get<Vector2>();

    public void OnTurn(InputValue val)
    {
        _turnDir = val.Get<Vector2>();
        //TODO: make turn acceleration as it is in doom 93 (holding down run makes you turn faster, accelerates harshly)
    }
}

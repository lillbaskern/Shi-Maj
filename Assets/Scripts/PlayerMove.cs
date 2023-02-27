using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Camera _cam;
    public float LookSensitivity = 4;
    CharacterController _cc;



    [SerializeField] float _jumpHeight = 1;
    [SerializeField] float _fallSpeed = 0.5f;
    [SerializeField] float _moveSpeed = 5f;


    [SerializeField] Vector2 _inputDir;
    Vector2 _turnDir;

    float _verticalVel;
    float _lookAccel = 1;

    public bool IsGrounded { get; private set; }

    void Awake() => _cc = GetComponent<CharacterController>();
    void Update()
    {
        IsGrounded = _cc.isGrounded;
        if (IsGrounded) _verticalVel = 0;
        Vector3 _moveDir = new Vector3(_inputDir.x * _moveSpeed, _verticalVel, _inputDir.y * _moveSpeed);
        _verticalVel = Mathf.MoveTowards(_verticalVel, Physics.gravity.y, Time.deltaTime * _fallSpeed);

        _cc.transform.Rotate(Vector3.up * _turnDir.x * _lookAccel * (Time.deltaTime * 100));
        _cc.Move(_cc.transform.rotation * _moveDir * Time.deltaTime);
    }



    public void OnMove(InputValue val) => _inputDir = val.Get<Vector2>();

    public void OnTurn(InputValue val)
    {
        _turnDir = val.Get<Vector2>();
        if (_turnDir == Vector2.zero)
        {
            _lookAccel = 1;
            return;
        }
        _lookAccel += Time.deltaTime * 50;
    }
}

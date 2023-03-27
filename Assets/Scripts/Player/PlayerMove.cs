using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

//<SUMMARY>
//CLASS WHICH HANDLES PLAYER MOVEMENT
//IS INHERITED BY ANY CHARACTER
//also contains certain character properties which just cant be put into an interface
//namely string _name
public class PlayerMove : PlayerShoot
{
    //Unityevents to signal the animator to enter certain animation states
    public UnityEvent Land;
    public UnityEvent JumpPressed;

    Animator _weaponAnimator;


    [SerializeField] protected string _name;
    Camera _cam;


    //keeping this public for now. i doubt any gameplay method would even get close to changing this variable anyways
    public float LookSensitivity = 4;


    [SerializeField] protected float _jumpHeight = 100;
    [SerializeField] protected float _fallSpeed = 0.5f;
    [SerializeField] protected float _moveSpeed = 5f;


    [SerializeField, Range(0.1f, 15f)] float _maxSpeed = 5f;
    [SerializeField, Range(0.1f, 1)] float _minSpeed = 5f;
    [SerializeField, Tooltip("How fast the player accelerates"), Range(0.1f, 30f)] float _moveAccel = 10f;


    float _turnRate;
    [SerializeField] float _turnAccel;
    [SerializeField] float _minTurnRate = 0.5f;
    [SerializeField] float _maxTurnRate = 10f;


    [SerializeField] Vector2 _inputDir;


    private Vector2 _turnDir;


    Vector3 _moveDir;
    private float _verticalVel;
    [SerializeField] float _baseCoyoteTime;
    private float _currCoyoteTime = 0;


    public static Collider2D _topTouchArea;
    public static Vector2 _topTouchAreaBeginPoint;
    Collider _bottomTouchArea;



    public bool IsGrounded { get; private set; }


    void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _topTouchAreaBeginPoint.y = Screen.height * 0.75f;
        _topTouchAreaBeginPoint.x = Screen.width * 0.5f;
        _highCrosshair = GameObject.Find("HighCrosshairDecal").transform;
        _highCrosshair.position = _topTouchAreaBeginPoint;
    }

    //method which allows subchar scripts to add themselves to the character list during runtime
    protected void SendToCharList(ICharacter character)
    {
        if (PlayerHead.Characters.Contains(character)) return;
        PlayerHead.Characters.Add(character);
    }


    protected void MoveAndTurnLoop(InputAction turn, InputAction move, InputAction Jump)
    {
        //Set isgrounded
        IsGrounded = _cc.isGrounded;
        


        //read input vectors
        _turnDir = turn.ReadValue<Vector2>();
        _inputDir = move.ReadValue<Vector2>();

        if (!move.inProgress) _inputDir = Vector2.zero;

        ApplyDeadZone(ref _inputDir.x);

        if (IsGrounded)
        {
            _verticalVel = 0;
            _currCoyoteTime = _baseCoyoteTime;
        }
        //using either min or max is a great way of keeping variables below or above a certain threshhold
        else _currCoyoteTime = Mathf.Max(_currCoyoteTime - Time.deltaTime, 0);



        //jumping and gravity (prototype only)
        if (Jump.WasPressedThisFrame() && _currCoyoteTime > 0)
        {
            //invoke unity event which is wired to the player's animator
            JumpPressed.Invoke();

            _verticalVel = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);

            _currCoyoteTime = 0;
        }

        //gravity
        //you can further dissolve Physics.gravity.y into float _MaxFallSpeed 
        _verticalVel += Physics.gravity.y * Time.deltaTime;
        _verticalVel = Mathf.Clamp(_verticalVel, Physics.gravity.y, 50f);




        //handle rotation
        _cc.transform.Rotate(Vector3.up * _turnDir.x * (Time.deltaTime * _turnRate));

        //handle move acceleration

        HandleMoveAndTurnAccel();


        //create final move vector
        _moveDir = new Vector3(_inputDir.x * _moveSpeed, _verticalVel, _inputDir.y * _moveSpeed);
        _cc.Move(_cc.transform.rotation * _moveDir * Time.deltaTime);
    }

    private void HandleMoveAndTurnAccel()
    {
        if (_inputDir.magnitude <= 0)
        {
            _moveSpeed = Mathf.Max(_moveSpeed - _moveAccel * Time.deltaTime * 2f, _minSpeed);
        }

        else _moveSpeed = Mathf.Min(_moveSpeed + _moveAccel * Time.deltaTime, _maxSpeed);



        //handle turn acceleration
        if (_turnDir.magnitude <= 0)
        {
            _turnRate = Mathf.Max(_turnRate - _turnAccel * Time.deltaTime * 2f, _minTurnRate);
        }

        else _turnRate = Mathf.Min(_turnRate + _turnAccel * Time.deltaTime, _maxTurnRate);
    }

    public static void ApplyDeadZone(ref float input)
    {
        if (Mathf.Abs(input) < 0.2f) input = 0;
    }




}

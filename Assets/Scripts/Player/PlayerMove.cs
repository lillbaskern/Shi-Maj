using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//<SUMMARY>
//CLASS WHICH HANDLES PLAYER MOVEMENT
//IS INHERITED BY ANY CHARACTER
//also contains certain character properties which just cant be put into an interface
//namely string _name, 
public class PlayerMove : PlayerShoot
{
    [SerializeField] protected string _name;
    Camera _cam;
    //keeping this public for now. i doubt any gameplay method would even get close to changing this variable anyways
    public float LookSensitivity = 4;
    CharacterController _cc;

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

        if (!move.inProgress) _inputDir = Vector2.zero;


        //Set isgrounded
        IsGrounded = _cc.isGrounded;
        if (IsGrounded) _verticalVel = 0;



        //jumping and gravity (prototype only)
        if (Jump.WasPressedThisFrame() && IsGrounded)
        {
            _verticalVel += Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }
        //you can further dissolve Physics.gravity.y into float _MaxFallSpeed 
        _verticalVel += Physics.gravity.y * Time.deltaTime;
        _verticalVel = Mathf.Clamp(_verticalVel, Physics.gravity.y, 50f);




        //handle rotation
        _cc.transform.Rotate(Vector3.up * _turnDir.x * (Time.deltaTime * _turnRate));

        //handle move acceleration
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


        //create final move vector
        _moveDir = new Vector3(_inputDir.x * _moveSpeed, _verticalVel, _inputDir.y * _moveSpeed);
        _cc.Move(_cc.transform.rotation * _moveDir * Time.deltaTime);
    }


    //method for handling touch input. is going to make sure that the player doesnt drift 
    //could be deprecated
    void HandleTouch(Vector2 inputVector, Collider2D touchArea)
    {
        //make sure touch is within its designated area
        if (!touchArea.bounds.Contains(inputVector))
        {
            //setting _inputDir to zero here causes an abrupt stop to the player's movement
            //thats okay tho, as acceleration/deacceleration will be handled seperate from _inputDir
            _inputDir = Vector2.zero;
            return;
        }


        //TODO: FIND WAY TO DETERMINE WHETHER OR NOT THE PLAYER HAS STOPPED MOVING THEIR FINGER(S)
        //MAYBE: just hold a buffer of like 10 frames and check how approximately close they are. if theyre too close just return a vector2.zero or more the input vector towards zero
        //more thinking needed
        if (inputVector.normalized == Vector2.zero)
        {
            _inputDir = Vector2.zero;
            return;
        }
        _inputDir = inputVector - _topTouchAreaBeginPoint;

        _inputDir = _inputDir.normalized;
    }
}

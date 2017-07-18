using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{

    [Header("OtherClasses")]

    [SerializeField]
    private KeysInput HandleInput;

    public ChangeGravity _ChangeGravity;

    [Header("Objects")]
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Camera _camera;


    [Header("Values")]

    [SerializeField]
    private float _walkSpeed = 3f;

    [SerializeField]
    private float _RunSpeed = 6f;

    [SerializeField]
    private float _RotateSpeedX = 100f;

    [SerializeField]
    private float _RotateSpeedY = 5f;

    [SerializeField]
    private float _JumpForce = 30f;

    private float PositiveDegree = 75f;

    private float NegativeDegree = 75f;

    [Header("BoolInfo")]

    public bool _IsGrounded = false;

    public bool _ChangeGra = false;


    public Vector3 LastPosition;


    void Awake()
    {
        LastPosition = transform.position;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput.InputMenager();
    }

    void FixedUpdate()
    {
        DoMove();
        DoRotate();
        DoJump();
    }

    void DoMove()
    {
        Vector3 translationX;
        Vector3 translationY;

        if (HandleInput._IsRunning && _IsGrounded)
        {
            translationX = transform.forward * HandleInput._WalkXKey * _RunSpeed * Time.deltaTime;
            translationY = transform.right * HandleInput._WalkYKey * _RunSpeed * Time.deltaTime;
        }
        else
        {
          translationX = transform.forward * HandleInput._WalkXKey * _walkSpeed * Time.deltaTime;
          translationY = transform.right * HandleInput._WalkYKey * _walkSpeed * Time.deltaTime;
        }
        _rigidbody.MovePosition(transform.position + translationX + translationY);
    }
    void DoRotate()
    {
        Vector3 RotatingX;
        Quaternion RotatingY = _camera.transform.localRotation * Quaternion.Euler(Vector3.left * HandleInput._RotateY * _RotateSpeedY);
        if(_ChangeGra == false)
        {
            RotatingX = transform.up * HandleInput._RotateX * _RotateSpeedX * Time.deltaTime;
        }
        else
        {
            RotatingX = -transform.up * HandleInput._RotateX * _RotateSpeedX * Time.deltaTime;
        }
        
        _rigidbody.transform.Rotate(RotatingX);
        if (RotatingY.eulerAngles.x < NegativeDegree || RotatingY.eulerAngles.x > (360.0f - PositiveDegree))
        {
            _camera.transform.localRotation = RotatingY;
        }

        
    }
    void DoJump()
    {
        if (HandleInput._IsJumping && _IsGrounded)
        {
            Vector3 Jumping;
            Jumping = transform.up * _JumpForce * Time.deltaTime;
            _rigidbody.AddForce(Jumping, ForceMode.Impulse);
        }
        _IsGrounded = false;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            _IsGrounded = true;
        }
    }
}

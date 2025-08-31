using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : PlayerBase
{
    [Header("Move")]
    [SerializeField] private float walkSpeed;
    private Vector2 _currentMove;
    private bool isMove;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform cameraPos;
    private bool isGround;

    private Rigidbody _rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        base.BaseAwake();
    }

    //イベント登録
    private void OnEnable()
    {
        _playerBase.Player.Move.performed += OnInputMove;
        _playerBase.Player.Move.canceled += OnInputMove;
        _playerBase.Player.Jump.started += OnInputJump;
    }
    //イベント解除
    private void OnDisable()
    {
        _playerBase.Player.Move.performed += OnInputMove;
        _playerBase.Player.Move.canceled += OnInputMove;
        _playerBase.Player.Jump.started += OnInputJump;
    }
    void FixedUpdate()
    {
        Vector3 foward = cameraPos.forward;
        foward.y = 0f;

        if (cameraPos.forward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(foward);
        }
        if (isMove)
        {
            Vector3 orientation = cameraPos.forward * _currentMove.y + cameraPos.right * _currentMove.x;
            Vector3 currentVelocity = orientation.normalized * walkSpeed;
            _rb.linearVelocity = new Vector3(currentVelocity.x, _rb.linearVelocity.y, currentVelocity.z);
        }
    }
    private void OnInputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentMove = context.ReadValue<Vector2>();
            isMove = true;
        }
        else if (context.canceled)
        {
            _currentMove = Vector2.zero;
            isMove = false;
        }
    }
    private void OnInputJump(InputAction.CallbackContext context)
    {
        if (context.started && isGround)
        {
            _rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
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
    [SerializeField] private float reCastTime;
    private float _jumptime = 0f;
    private bool isGround;

    void Awake()
    {
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
        _jumptime += Time.deltaTime;

        if (isMove)
        {
            Vector3 move = _rb.linearVelocity;
            _rb.linearVelocity = new Vector3(_currentMove.x, 0f, _currentMove.y) * walkSpeed;
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
        if (context.started && _jumptime > reCastTime)
        {
            _rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
            _jumptime = 0f;
        }
    }
}
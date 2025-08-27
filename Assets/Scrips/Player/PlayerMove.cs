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

    [SerializeField] private Transform cameraPos;
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
        if (context.started && _jumptime > reCastTime)
        {
            _rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
            _jumptime = 0f;
        }
    }
}
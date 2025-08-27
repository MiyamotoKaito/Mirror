using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : PlayerBase
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float reCastTime;
    private float jumptime = 0f;
    private bool isGround;

    void Awake()
    {
        base.BaseAwake();
    }

    void FixedUpdate()
    {
        jumptime += Time.deltaTime;

        //移動処理
        if (_playerBase.Player.Move.triggered)
        {
            Vector2 move = _playerBase.Player.Move.ReadValue<Vector2>();
            _rb.linearVelocity = new Vector3(move.x, 0f, move.y) * moveSpeed;
        }
        //ジャンプ処理
        else if (_playerBase.Player.Jump.triggered && jumptime > reCastTime)
        {
            _rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
            jumptime = 0f;
        }
    }
}
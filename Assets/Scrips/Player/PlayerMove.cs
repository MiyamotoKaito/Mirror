using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの動きに関するクラス
/// </summary>
public class PlayerMove : PlayerBase
{
    [Header("Move")]
    [SerializeField] private float walkSpeed;
    private Vector2 _currentMove;
    /// <summary>動いても良いか判断するためのフラグ</summary>
    private bool isMove;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    /// <summary>ジャンプしていいかのフラグ</summary>
    private bool isGround;

    [SerializeField] private Transform cameraPos;
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
        _playerBase.Player.Move.performed -= OnInputMove;
        _playerBase.Player.Move.canceled -= OnInputMove;
        _playerBase.Player.Jump.started -= OnInputJump;
        base.BaseOnDisable();
    }
    void FixedUpdate()
    {
        //カメラのフォワードを取る
        Vector3 foward = cameraPos.forward;
        foward.y = 0f;

        if (cameraPos.forward != Vector3.zero)
        {
            //カメラのフォワードにカメラを向かせる
            transform.rotation = Quaternion.LookRotation(foward);
        }
        if (isMove)
        {
            //プレイヤーの向いている方向を取る
            Vector3 orientation = transform.forward * _currentMove.y + cameraPos.right * _currentMove.x;
            //向いている方向の速度に歩いているスピードを掛ける
            Vector3 currentVelocity = orientation.normalized * walkSpeed;
            //現在の速度にx軸とz軸を加え続ける
            _rb.linearVelocity = new Vector3(currentVelocity.x, _rb.linearVelocity.y, currentVelocity.z);
        }
    }
    /// <summary>
    /// 長押しされてたら動いていいフラグを立てて、押されてなかったら止まる
    /// </summary>
    /// <param name="context"></param>
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
    /// <summary>
    /// ジャンプを押したら上方向に力を加える
    /// </summary>
    /// <param name="context"></param>
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
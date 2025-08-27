using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected InputSystem_Actions _playerBase;
    protected Rigidbody _rb;

    protected void BaseAwake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerBase = new InputSystem_Actions();
        _playerBase.Enable();
    }
}
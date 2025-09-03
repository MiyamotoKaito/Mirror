using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected InputSystem_Actions _playerBase;
    protected void BaseAwake()
    {
        _playerBase = new InputSystem_Actions();
        _playerBase.Enable();
    }

    protected void BaseOnDisable()
    {
        _playerBase?.Disable();
    }
}
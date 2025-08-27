using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected InputSystem_Actions _playerBase;

    protected void Awake()
    {
        _playerBase = new InputSystem_Actions();
        _playerBase.Enable();
    }
}

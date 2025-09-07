using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Switchのオブジェクトにアタッチして対応するドアのアニメーションを起動する
/// </summary>
public class DoorSwitch : PlayerBase
{
    public UnityEvent Action;
    private bool isTrigger;

    private void Awake()
    {
        base.BaseAwake();
    }

    private void OnEnable()
    {
        _playerBase.Player.Interact.started += OnInputInteract;
    }

    private void OnDisable()
    {
        _playerBase.Player.Interact.started -= OnInputInteract;
        base.BaseOnDisable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
    /// <summary>
    /// ドアのアニメーションの開始
    /// </summary>
    /// <param name="context"></param>
    private void OnInputInteract(InputAction.CallbackContext context)
    {
        if (context.started && isTrigger)
        {
            Action.Invoke();
        }
    }
}
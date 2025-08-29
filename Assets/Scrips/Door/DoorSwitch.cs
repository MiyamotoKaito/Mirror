using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Swicth"))
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Swicth"))
        {
            isTrigger = false;
        }
    }
    private void OnInputInteract(InputAction.CallbackContext context)
    {
        if (context.started && isTrigger)
        {
            Action.Invoke();
        }
    }
}
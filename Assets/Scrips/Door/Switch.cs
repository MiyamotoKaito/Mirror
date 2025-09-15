using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Switchのオブジェクトにアタッチして対応するドアのアニメーションを起動する
/// </summary>
public class Switch : PlayerBase
{
    [SerializeField] private Swicthstate swicthstate;
    public UnityEvent Action;
    private bool isTrigger;

    private GameObject _player;
    private AudioSource _audioSource;
    private void Awake()
    {
        base.BaseAwake();
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = _player.GetComponent<AudioSource>();
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
    /// 入力を受け付けた時にイベントで呼び出す
    /// </summary>
    /// <param name="context"></param>
    private void OnInputInteract(InputAction.CallbackContext context)
    {
        if (context.started && isTrigger)
        {
            Action.Invoke();
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (swicthstate == Swicthstate.buttom)
        {
            AudioManager.Instance.PlaySE("ボタンを押す", _audioSource);
        }
        else if (swicthstate == Swicthstate.star)
        {
            AudioManager.Instance.PlaySE("星ゲット", _audioSource);
        }
    }
}

public enum Swicthstate
{
    buttom,
    star
}
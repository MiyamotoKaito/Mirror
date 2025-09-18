using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Switchのオブジェクトにアタッチして対応するドアのアニメーションを起動する
/// </summary>
public class Switch : PlayerBase
{
    [SerializeField] private SwicthState swicthState;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Image interactPanel;
    public UnityEvent Action;
    private Animator _animator;
    private GameObject _player;
    private AudioSource _playerAudioSource;
    private bool isTrigger;
    private bool isPlaying;
    private bool isPlaySound;
    private string interactName;
    private void Awake()
    {
        base.BaseAwake();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerAudioSource = _player.GetComponent<AudioSource>(); 
    }
    private void Start()
    {
        interactPanel.gameObject.SetActive(false);
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
            InputInteractName();
            if (!isPlaying)
            {
                interactPanel.gameObject.SetActive(true);
            }  
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            interactPanel.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 入力を受け付けた時にイベントで呼び出す
    /// </summary>
    /// <param name="context"></param>
    private void OnInputInteract(InputAction.CallbackContext context)
    {
        if (context.started && isTrigger && !isPlaying)
        {
            Action.Invoke();
            PlaySound();
            isPlaying = true;
            interactPanel.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 一回だけ音を鳴らす
    /// stateによって鳴らす音を変える
    /// goalは音を鳴らさない
    /// /// </summary>
    private void PlaySound()
    {
        if (!isPlaySound)
        {
            if (swicthState == SwicthState.button)
            {
                //ボタンのアニメーション
                _animator.SetBool("Push", true);
                AudioManager.Instance.PlaySE("ボタンを押す", _playerAudioSource);
            }
            else if (swicthState == SwicthState.star)
            {
                AudioManager.Instance.PlaySE("星ゲット", _playerAudioSource);
            }
            isPlaySound = true;
        }
    }
    private void InputInteractName()
    {
        if (swicthState == SwicthState.button)
        {
            interactName = "押す";
        }
        if (swicthState == SwicthState.star)
        {
            interactName = "ゲット";
        }
        if (swicthState == SwicthState.goal)
        {
            interactName = "ゴール";
        }
        interactText.text = interactName;
    }
}
public enum SwicthState
{
    button,
    star,
    goal
}
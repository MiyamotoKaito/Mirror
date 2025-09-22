using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwicthMirror : PlayerBase
{
    /// <summary>タイトルのカメラ</summary>
    [SerializeField] private CinemachineCamera titleCam;
    /// <summary>タイトルで鏡の周りを周ってるカメラ</summary>
    [SerializeField] private CinemachineCamera moveCam;
    /// <summary>タイトルで中心の鏡についているカメラ</summary>
    [SerializeField] private CinemachineCamera mirrorCam;
    /// <summary>タイトルで見えるカメラ</summary>
    [SerializeField] private Camera mainCam;
    /// <summary>鏡の中に入ったら切り替えるカメラ</summary>
    [SerializeField] private Camera selectCam;
    /// <summary>フェードイン用のパネル</summary>
    [SerializeField] private Image panel;
    [SerializeField] private Image logo;

    private AudioSource _audioSource;
    public UnityEvent Action;

    private void Awake()
    {
        base.BaseAwake();
        titleCam.gameObject.SetActive(true);
        selectCam.gameObject.SetActive(false);
        moveCam.gameObject.SetActive(false);
        mirrorCam.gameObject.SetActive(false);
        panel.enabled = false;
        _audioSource = AudioManager.Instance.gameObject.GetComponent<AudioSource>();
        AudioManager.Instance.PlayBGM("Title", _audioSource);
        logo.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        _playerBase.Player.All.started += OnInputEnterMirror;
    }

    private void OnDisable()
    {
        _playerBase.Player.All.started -= OnInputEnterMirror;
        base.BaseOnDisable();
    }
    /// <summary>
    /// クリックすると鏡の中に入る
    /// </summary>
    /// <param name="context"></param>
    private void OnInputEnterMirror(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Swicth());
            logo.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 鏡の中に入った後のアニメーション
    /// </summary>
    /// <returns></returns>
    private IEnumerator Swicth()
    {
        AudioManager.Instance.Stop(_audioSource);
        titleCam.gameObject.SetActive(false);
        moveCam.gameObject.SetActive(true);
        AudioManager.Instance.PlaySE("ワープ", _audioSource);
        panel.enabled = true;
        panel.DOFade(0, 0);

        yield return new WaitForSeconds(1);
        moveCam.gameObject.SetActive(false);
        mirrorCam.gameObject.SetActive(true);
        panel.DOFade(1, 1);

        yield return new WaitForSeconds(1);
        panel.DOFade(0, 1);
        mirrorCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(false);
        selectCam.gameObject.SetActive(true);

        yield return null;
        Action.Invoke();
    }
}
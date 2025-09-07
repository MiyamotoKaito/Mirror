using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    public UnityEvent Action;

    private void Awake()
    {
        base.BaseAwake();
        titleCam.gameObject.SetActive(true);
        selectCam.gameObject.SetActive(false);
        moveCam.gameObject.SetActive(false);
        mirrorCam.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _playerBase.Player.Attack.started += OnInputEnterMirror;
    }

    private void OnDisable()
    {
        _playerBase.Player.Attack.started -= OnInputEnterMirror;
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
        }
    }
    /// <summary>
    /// 鏡の中に入った後のアニメーション
    /// </summary>
    /// <returns></returns>
    private IEnumerator Swicth()
    {
        titleCam.gameObject.SetActive(false);
        moveCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        moveCam.gameObject.SetActive(false);
        mirrorCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        mirrorCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(false);
        selectCam.gameObject.SetActive(true);

        yield return null; ;
        Action.Invoke();
    }
}
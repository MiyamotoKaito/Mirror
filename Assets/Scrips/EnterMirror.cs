using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwicthMirror : PlayerBase
{
    [SerializeField] private CinemachineCamera titleCam;
    [SerializeField] private CinemachineCamera moveCam;
    [SerializeField] private CinemachineCamera mirrorCam;
    [SerializeField] private CinemachineCamera selectView;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera selectCam;
    [SerializeField] private Camera showCam;

    private void Awake()
    {
        base.BaseAwake();
        selectView.gameObject.SetActive(false);
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
        _playerBase.Player.Attack.started += OnInputEnterMirror;
    }

    private void OnInputEnterMirror(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(Swicth());
        }
    }

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

        yield return null;
        selectView.gameObject.SetActive(true);
    }
}
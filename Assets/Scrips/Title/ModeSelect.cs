using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// カメラの位置を切り替えるクラス
/// </summary>
public class ModeSelect : PlayerBase
{
    /// <summary>シーンがついているカメラ</summary>
    [SerializeField] private List<CinemachineCamera> Cams;
    private int cameraCount = 0;//最初のカメラの優先度

    private void Awake()
    {
        base.BaseAwake();
        Cams[0].Priority = 1;//最初のカメラの優先度を1にして他のカメラの衝突を避ける
    }
    private void OnEnable()
    {
        _playerBase.Player.Right.started += OnInputRightCam;
        _playerBase.Player.Left.started += OnInputLeftCam;
    }
    private void OnDisable()
    {
        _playerBase.Player.Right.started -= OnInputRightCam;
        _playerBase.Player.Left.started -= OnInputLeftCam;
        base.BaseOnDisable();
    }
    /// <summary>
    /// インプットシステムのRightを押すとカメラを右に切り替える
    /// </summary>
    /// <param name="context"></param>
    private void OnInputRightCam(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //現在のカメラの優先度を下げる
            Cams[cameraCount].Priority -= 1;
            cameraCount++;

            //範囲外に出ようとしたら最初戻す
            if (cameraCount >= Cams.Count)
            {
                cameraCount = 0;
            }

            //次のカメラの優先度を上げる
            Cams[cameraCount].Priority += 1;
        }
    }
    /// <summary>
    /// インプットシステムのLeftを押すとカメラを左に切り替える
    /// </summary>
    /// <param name="context"></param>
    private void OnInputLeftCam(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //現在のカメラの優先度を下げる
            Cams[cameraCount].Priority -= 1;
            cameraCount--;

            //範囲外に出ようとしたら最初戻す
            if (cameraCount <= -1)
            {
                cameraCount = Cams.Count - 1;
            }

            //次のカメラの優先度を上げる
            Cams[cameraCount].Priority += 1;
        }
    }
}

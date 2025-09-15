using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// カメラの動きを制御するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    private CinemachineInputAxisController _cameraAxis;
    private GameObject _camera;
    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("Camera");
        _cameraAxis = _camera.GetComponent<CinemachineInputAxisController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reflection"))
        {
            ChangeGain();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reflection"))
        {
            ChangeGain();
        }
    }
    /// <summary>
    /// 視点操作を反転させるメソッド
    /// </summary>
    private void ChangeGain()
    {
        //cinemachineInputAxisControllerから軸を取得
        var controllers = _cameraAxis.Controllers;

        foreach (var controller in controllers)
        {
            Debug.Log("反転");
            controller.Input.Gain *= -1;
        }
    }
}

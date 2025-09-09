using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// オブジェクトの周りを回転させるクラス
/// </summary>
public class RotateMirror : MonoBehaviour
{
    /// <summary>回転させる軸</summary>
    [SerializeField] private Vector3 axis;
    /// <summary>軸となるオブジェクト</summary>
    [SerializeField] private GameObject titleMirror;
    /// <summary>周るスピード</summary>
    [SerializeField] private float rotateSpeed;
    /// <summary>回転させるカメラ</summary>
    [SerializeField] private CinemachineCamera titleCam;
    private void FixedUpdate()
    {
        ///カメラをオブジェクトを軸にして周囲を周る処理
        titleCam.transform.RotateAround(titleMirror.transform.position, axis, rotateSpeed);
    }
}
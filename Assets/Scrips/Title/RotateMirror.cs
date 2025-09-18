using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// オブジェクトの周りをカメラで周回させるクラス
/// </summary>
public class RotateMirror : ObjectRotate
{
    /// <summary>周るスピード</summary>
    [SerializeField] private float rotateSpeed;
    /// <summary>回転させるカメラ</summary>
    [SerializeField] private CinemachineCamera titleCam;
    private void FixedUpdate()
    {
        ///カメラをオブジェクトを軸にして周囲を周る処理
        titleCam.transform.RotateAround(axisObject.transform.position, axis, rotateSpeed);
    }
}
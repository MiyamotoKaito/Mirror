using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    /// <summary>垂直画角の数値</summary>
    const float verticalAngle = 0.7f;

    public int CamMode;

    private Camera mirrorCamera;
    private void Start()
    {
        mirrorCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        mirrorCamera.nearClipPlane = playerCamera.nearClipPlane;

        //プレイヤーの視野と鏡のカメラの視野を同期
        if (CamMode == 0)
            mirrorCamera.fieldOfView = playerCamera.fieldOfView;

        //カメラの視野の比率を変更する
        if (CamMode == 1)
            mirrorCamera.aspect = verticalAngle;
    }
}

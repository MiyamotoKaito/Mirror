/// <summary>
/// ワープする鏡用のクラス
/// </summary>
public class Incident : MirrorController
{
    public override void UpdateMirror()
    {
        //カメラから鏡面への方向ベクトル
        var incident = playerCamera.transform.forward;

        //プレイヤーと同じ方向を向かせる
        reflectionCamera.transform.LookAt(reflectionCamera.transform.position + incident);

        //鏡のスケール感を統一
        reflectionCamera.fieldOfView = playerCamera.fieldOfView;
        reflectionCamera.nearClipPlane = playerCamera.nearClipPlane;
    }
}
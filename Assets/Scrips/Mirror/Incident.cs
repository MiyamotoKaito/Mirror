using UnityEngine;

public class Incident : MirrorController
{
    public override void UpdateMirror()
    {
        //鏡面からカメラへの方向ベクトル
        var incident = playerCamera.transform.forward;

        //プレイヤーと同じ方向を向かせる
        reflectionCamera.transform.LookAt(reflectionCamera.transform.position + incident);
    }
}

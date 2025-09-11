using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 鏡の反射を管理するクラス
/// </summary>
public class SpecularReflection : MirrorController
{
    public override void UpdateMirror()
    {
        //反射ベクトルの計算
        #region
        //カメラから鏡面への方向ベクトル
        var incident = reflectionCamera.transform.position - playerCamera.transform.position;

        //鏡面からの法線ベクトル
        var normal = transform.forward;

        //カメラの反射ベクトル
        //Reflect関数っていうのがあるらしい
        var reflection = (incident + 2 * (Vector3.Dot(-incident, normal)) * normal);
        #endregion

        //反射ベクトルの方向に鏡面のカメラを向かせる
        reflectionCamera.transform.LookAt(reflectionCamera.transform.position + reflection);

        //カメラと鏡面との距離(焦点距離)
        var dictance = Vector3.Distance(transform.position, playerCamera.transform.position);

        //鏡のスケール感を統一
        reflectionCamera.fieldOfView = playerCamera.fieldOfView;
        reflectionCamera.nearClipPlane = playerCamera.nearClipPlane;
    }
}
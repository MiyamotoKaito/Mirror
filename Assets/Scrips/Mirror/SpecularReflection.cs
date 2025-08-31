using System;
using UnityEngine;

public class SpecularReflection : MonoBehaviour
{
    [Tooltip("playerのカメラ"), SerializeField]
    private Transform playerCamera;
    [Tooltip("反射先を写すカメラ"), SerializeField]
    private Transform reflectionCamera;

    public static event Action OnMirorUpdate;

    private void OnEnable()
    {
        OnMirorUpdate += UpdateMirror;
    }
    private void OnDisable()
    {
        OnMirorUpdate -= UpdateMirror;
    }
    private void Start()
    {
    }
    private void Update()
    {
        UpdateMirror();
    }

    private void UpdateMirror()
    {
        //反射ベクトルの計算
        #region
        //カメラから鏡面への方向ベクトル
        var incident =(reflectionCamera.position - playerCamera.position);

        //鏡面からの法線ベクトル
        var normal = transform.forward;

        //カメラの反射ベクトル
        //Reflect関数っていうのがあるらしい
        var reflection = (incident + 2 * (Vector3.Dot(-incident, normal)) * normal);
        #endregion

        //反射ベクトルの方向に鏡面のカメラを向かせる
        reflectionCamera.LookAt(reflectionCamera.position + reflection);
    }
}
using System;
using UnityEngine;

public class SpecularReflection : MonoBehaviour
{
    [Tooltip("playerのカメラ"), SerializeField]
    private Camera playerCamera;
    [Tooltip("反射先を写すカメラ"), SerializeField]
    private Camera reflectionCamera;

    [SerializeField]
    private float size;

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
        var incident = (reflectionCamera.transform.position - playerCamera.transform.position);

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
    }
}
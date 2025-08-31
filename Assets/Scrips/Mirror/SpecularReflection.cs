using System;
using UnityEngine;

[ExecuteAlways]
public class SpecularReflection : MonoBehaviour
{
    [Tooltip("playerのカメラ"), SerializeField]
    private Camera playerCamera;
    [Tooltip("反射先を写すカメラ"), SerializeField]
    private Camera reflectionCamera;

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
        var normal = reflectionCamera.transform.forward;

        //カメラの反射ベクトル
        var reflection = (incident + 2 * (Vector3.Dot(-incident, normal)) * normal);
        #endregion

        //反射ベクトルの方向に鏡面のカメラを向かせる
        reflectionCamera.transform.LookAt(reflection);
    }
}

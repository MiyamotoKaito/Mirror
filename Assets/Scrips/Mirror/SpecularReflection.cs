using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Rendering;

[ExecuteAlways]//playmodeでなくても作動する
public class SpecularReflection : MonoBehaviour
{
    [Tooltip("playerのカメラ"), SerializeField]
    private Camera targetCamera;
    [Tooltip("反射先を写すカメラ"), SerializeField]
    private Camera reflectionCamera;

    [SerializeField] private Transform specular;
    [SerializeField] private Transform frame;
    [SerializeField] private float size;
    [SerializeField] private bool enabledTarget;

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
        if (targetCamera == null)
        {
            targetCamera = FindFirstObjectByType<Camera>();
        }
        if (reflectionCamera == null)
        {
            reflectionCamera = GetComponentInChildren<Camera>();
        }
    }
    private void Reset()
    {
        reflectionCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        OnMirorUpdate?.Invoke();
    }

    private void UpdateMirror()
    {
        //カメラから鏡面への方向ベクトル
        var incident = transform.position - targetCamera.transform.position;

        //鏡面からの法線ベクトル
        var normal = transform.forward;

        //カメラの反射ベクトル
        var reflection = incident + 2 * (Vector3.Dot(-incident, normal)) * normal;
    }
}

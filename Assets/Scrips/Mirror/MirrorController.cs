using UnityEngine;
using System;

/// <summary>
/// 鏡の共通する部分を管理するクラス
/// </summary>
public abstract class MirrorController : MonoBehaviour
{
    [Tooltip("playerのカメラ"), SerializeField]
    protected Camera playerCamera;
    [Tooltip("反射先を写すカメラ"), SerializeField]
    protected Camera reflectionCamera;

    public static event Action OnMirorUpdate;

    private void OnEnable()
    {
        OnMirorUpdate += UpdateMirror;
    }
    private void OnDisable()
    {
        OnMirorUpdate -= UpdateMirror;
    }
    private void Update()
    {
        if (OnMirorUpdate != null)
        {
            UpdateMirror();
        }
    }

    public abstract void UpdateMirror();
}

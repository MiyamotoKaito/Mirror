using UnityEngine;
using System;

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
    private void Start()
    {
        var rCamPos = reflectionCamera.transform.position;
        var pCamPosY = playerCamera.transform.localPosition.y;
        reflectionCamera.transform.position = new Vector3(rCamPos.x, pCamPosY - 0.2f, rCamPos.z);
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

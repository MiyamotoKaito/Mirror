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
    //private void Start()
    //{
    //    var rCamPos = reflectionCamera.transform.position;
    //    reflectionCamera.transform.position = new Vector3(rCamPos.x, transform.position.y, rCamPos.z);
    //}
    private void Update()
    {
        if (OnMirorUpdate != null)
        {
            UpdateMirror();
        }
    }

    public abstract void UpdateMirror();
}

using Unity.Cinemachine;
using UnityEngine;

public class RotateMirror : MonoBehaviour
{
    [SerializeField] private Vector3 axis;
    [SerializeField] private GameObject titleMirror;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private CinemachineCamera titleCam;
    private void FixedUpdate()
    {
       titleCam.transform.RotateAround(titleMirror.transform.position, axis, rotateSpeed);
    }
}
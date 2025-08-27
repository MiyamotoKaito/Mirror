using Unity.VisualScripting;
using UnityEngine;

public class MirrorWarp : MonoBehaviour
{
    [SerializeField,Tooltip("���[�v��̃|�W�V����(����̋�)")] private Transform spwanPoint; 
    private bool inMirror;

    private void OnTriggerEnter(Collider other)
    {
        if (inMirror == false)
        {
            Debug.Log("�����Ă���");
            other.transform.position = spwanPoint.position;
            inMirror = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inMirror = false;
        Debug.Log("�����o����");
    }
}

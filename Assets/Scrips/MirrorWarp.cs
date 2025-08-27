using Unity.VisualScripting;
using UnityEngine;

public class MirrorWarp : MonoBehaviour
{
    [SerializeField,Tooltip("ワープ先のポジション(相手の鏡)")] private Transform spwanPoint; 
    private bool inMirror;

    private void OnTriggerEnter(Collider other)
    {
        if (inMirror == false)
        {
            Debug.Log("入ってきた");
            other.transform.position = spwanPoint.position;
            inMirror = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inMirror = false;
        Debug.Log("抜け出した");
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class MirrorWarp : MonoBehaviour
{
    [SerializeField, Tooltip("ワープ先のポジション(相手の鏡)")] private Transform spwanPoint;

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("入ってきた");
        other.transform.position = spwanPoint.position;
    }
}

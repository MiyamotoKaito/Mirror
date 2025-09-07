using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ワープさせるクラス
/// </summary>
public class MirrorWarp : MonoBehaviour
{
    [SerializeField, Tooltip("ワープ先のポジション(相手の鏡)")]
    private Transform spwanPoint;

    private void OnTriggerEnter(Collider other)
    {
        //鏡の中に入ってきたら相手の鏡のポジションに移動する
        Debug.Log("入ってきた");
        other.transform.position = spwanPoint.position;
    }
}

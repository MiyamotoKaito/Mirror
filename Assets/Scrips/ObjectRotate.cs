using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    /// <summary>回転させる軸</summary>
    [SerializeField] protected Vector3 axis;
    /// <summary>軸となるオブジェクト</summary>
    [SerializeField] protected GameObject axisObject;
}
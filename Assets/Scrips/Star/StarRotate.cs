using UnityEngine;
/// <summary>
/// 星を回転させるクラス
/// </summary>
public class StarRotate : ObjectRotate
{
    /// <summary>周るスピード</summary>
    [SerializeField] private float rotateSpeed;
    private void Awake()
    {
        axisObject = this.gameObject;
    }
    private void FixedUpdate()
    {
        transform.Rotate(axis * rotateSpeed * Time.fixedDeltaTime);
    }
}

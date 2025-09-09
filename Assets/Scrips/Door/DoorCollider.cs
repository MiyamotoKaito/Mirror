using UnityEngine;

/// <summary>
/// 鏡を見たときにイベントでドアを無効化するクラス
/// </summary>
public class DoorCollider : MonoBehaviour
{
    private Collider _door;
    private void Awake()
    {
        _door = GetComponent<Collider>();
    }
    /// <summary>
    /// ドアのコライダーを無効化するメソッド
    /// </summary>
    public void ColliderDisActive()
    {
        //鏡のコライダーを無効化して通れるようにする
        _door.enabled = false;
    }
    /// <summary>
    /// ドアそのものを消す
    /// </summary>
    public void ObjectDisActive()
    {
        this.gameObject.SetActive(false);
    }
}
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ドアを開けるためのクラスDoorSwitchクラスのイベントで呼び出す
/// </summary>
public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;

    public void OpenTheDoor()
    {
        right.DOLocalMove(new Vector3(-0.2f, 0, 0), 1f).SetRelative(true)
            .OnComplete(() => { right.DOLocalMove(new Vector3(0, 0, -2), 1f).SetRelative(true); });
        left.DOLocalMove(new Vector3(-0.2f, 0, 0), 1f).SetRelative(true)
            .OnComplete(() => { left.DOLocalMove(new Vector3(0, 0, 2), 1f).SetRelative(true); });
    }
}

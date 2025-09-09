using DG.Tweening;
using UnityEngine;

/// <summary>
/// ドアを開けるためのクラス
/// DoorSwitchクラスのイベントで呼び出す
/// </summary>
public class DoorOpen : MonoBehaviour
{
    /// <summary>右のドア</summary>
    [SerializeField] private Transform right;
    /// <summary>左のドア</summary>
    [SerializeField] private Transform left;
    /// <summary>ドアのanimation用のカメラ</summary>
    [SerializeField] private GameObject doorCamera;
    /// <summary>プレイヤーのカメラ</summary>
    private GameObject playerCam;
    /// <summary>animationを始めるためのフラグ</summary>
    private bool isOpen;
    private void Awake()
    {
        doorCamera.SetActive(false);
        playerCam = GameObject.Find("PlayerCamera");
    }
    /// <summary>
    /// ドアのアニメーション
    /// DoorSwicthイベントで呼び出す
    /// </summary>
    public void OpenTheDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            playerCam.SetActive(false);
            doorCamera.SetActive(true);

            //右のドアのanimation
            Tween r = right.DOLocalMove(new Vector3(0.2f, 0, 0), 1f).SetRelative(true)
                 .OnComplete(() =>
                 {
                     right.DOLocalMove(new Vector3(0, 0, -2), 1f).SetRelative(true)
                    .OnComplete(() =>
                    {
                        doorCamera.SetActive(false);
                    });
                 });
            //左のドアのanimation
            Tween l = left.DOLocalMove(new Vector3(0.2f, 0, 0), 1f).SetRelative(true)
                 .OnComplete(() =>
                 {
                     left.DOLocalMove(new Vector3(0, 0, 2), 1f).SetRelative(true)
                    .OnComplete(() =>
                    {
                        playerCam.SetActive(true);
                    });
                 });

            //アニメーションのディレイ
            r.SetDelay(2f);
            l.SetDelay(2f);
        }
    }
}

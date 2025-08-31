using DG.Tweening;
using UnityEngine;

/// <summary>
/// ドアを開けるためのクラス
/// DoorSwitchクラスのイベントで呼び出す
/// </summary>
public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private GameObject doorCamera;
    private GameObject playerCam;
    private bool isOpen;


    private void Awake()
    {
        doorCamera.SetActive(false);
        playerCam = GameObject.Find("PlayerCamera");
    }
    public void OpenTheDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            playerCam.SetActive(false);
            doorCamera.SetActive(true);


            Tween r = right.DOLocalMove(new Vector3(0.2f, 0, 0), 1f).SetRelative(true)
                 .OnComplete(() =>
                 {
                     right.DOLocalMove(new Vector3(0, 0, -2), 1f).SetRelative(true)
                    .OnComplete(() =>
                    {
                        doorCamera.SetActive(false);
                    });
                 });
            Tween l = left.DOLocalMove(new Vector3(0.2f, 0, 0), 1f).SetRelative(true)
                 .OnComplete(() =>
                 {
                     left.DOLocalMove(new Vector3(0, 0, 2), 1f).SetRelative(true)
                    .OnComplete(() =>
                    {
                        playerCam.SetActive(true);
                    });
                 });

            r.SetDelay(2f);
            l.SetDelay(2f);
        }
    }
}

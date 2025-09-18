using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    /// <summary>フェードインフェードアウト用のパネル</summary>
    [SerializeField] private Image panel;
    /// <summary>プレイヤーのカメラ</summary>
    private GameObject _playerCam;
    /// <summary>animationを始めるためのフラグ</summary>
    private bool isOpen;
    private Animator _animator;
    private AudioSource _audioSource;
    private void Awake()
    {
        doorCamera.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = player.GetComponent<AudioSource>();
        panel.gameObject.SetActive(false);
        _playerCam = GameObject.Find("PlayerCamera");
    }
    /// <summary>
    /// ドアのアニメーション
    /// DoorSwicthイベントで呼び出す
    /// </summary>
    private void OpenTheDoor()
    {
        Debug.Log("ドアオープン");
        
        if (!isOpen)
        {
            //一回だけにする
            isOpen = true;
            //音を鳴らす
            StartCoroutine(PlaySound());
            //カメラ切り替え
            _playerCam.SetActive(false);
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
                        _playerCam.SetActive(true);
                    });
                 });

            //ドアのアニメーションのディレイ
            r.SetDelay(2f);
            l.SetDelay(2f);
        }
    }
    public void PlayAnimation()
    {
        panel.gameObject.SetActive(true);
        panel.DOFade(1, 1).SetDelay(1)
            .OnComplete(() => 
            {
                panel.DOFade(0, 1);
                OpenTheDoor();
            });

    }
    private IEnumerator PlaySound()
    {
        AudioManager.Instance.PlaySE("煙", _audioSource);
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySE("ドアが開く", _audioSource);
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlaySE("ドアが開く", _audioSource);
    }
}
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{

    [SerializeField] private List<Stars> _stars;//星共の配列
    [SerializeField] private GameObject reflectMirror;
    [SerializeField] private CinemachineCamera allStarCam;
    [SerializeField] private CinemachineCamera mirrorCam;
    [SerializeField] private Image panel;
    [SerializeField] private GameObject uiArea;
    private CinemachineCamera _playerCam;

    [System.Serializable]
    public class Stars
    {
        /// <summary>プレイヤーが入手するための星</summary>
        [SerializeField] private GameObject star;
        /// <summary>どの星をゲットしたか確認するためのオブジェクト</summary>
        [SerializeField] private GameObject collectStar;
        [SerializeField] private CinemachineCamera starCam;
        private bool isCollected;

        public GameObject Star { get => star; }
        public GameObject CollectStar { get => collectStar; }
        public CinemachineCamera StarCam { get => starCam; }
        public bool IsCollected { get => isCollected; set => isCollected = value; }
    }

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Camera");
        _playerCam = player.GetComponent<CinemachineCamera>();
        foreach (var star in _stars)
        {
            star.Star.SetActive(true);
            star.CollectStar.SetActive(false);
            star.StarCam.Priority = 0;
        }
        reflectMirror.SetActive(true);
        mirrorCam.Priority = 0;
        uiArea.SetActive(true);
    }
    /// <summary>
    /// 星をゲットしたらゴール前の星を表示する
    /// </summary>
    public void ActiveCollectStar()
    {
        StartCoroutine(SetCam());
    }
    /// <summary>
    /// 全てのCollectStarが有効になっているかチェックして道を開ける
    /// </summary>
    private void CheckStars()
    {
        foreach (var star in _stars)
        {
            if (!star.CollectStar.activeSelf)
            {
                return;
            }
        }
        panel.gameObject.SetActive(true);
        panel.DOFade(1, 1).OnComplete(() =>
        {
            _playerCam.Priority = 0;
            mirrorCam.Priority = 1;
            uiArea.SetActive(false);
            panel.DOFade(0, 1).OnComplete(() =>
            {
                reflectMirror?.SetActive(false);

                panel.DOFade(1, 1).OnComplete(() =>
                {
                    _playerCam.Priority = 1;
                    mirrorCam.Priority = 0;
                    panel.DOFade(0, 1)
                        .OnComplete(() => { panel.gameObject.SetActive(false); });
                });
            });
        });
    }
    private IEnumerator SetCam()
    {

        foreach (var star in _stars)
        {
            if (!star.Star.activeSelf && !star.IsCollected)
            {
                //フェードイン
                panel.gameObject.SetActive(true);
                yield return panel.DOFade(1, 1).WaitForCompletion();

                // カメラ切り替え + フェードアウト
                _playerCam.Priority = 0;
                allStarCam.Priority = 1;
                yield return panel.DOFade(0, 1).WaitForCompletion();

                yield return new WaitForSeconds(1);

                // 個別カメラに切り替え
                allStarCam.Priority = 0;
                star.StarCam.Priority = 1;

                yield return new WaitForSeconds(1);

                // スター表示
                star.CollectStar.SetActive(true);

                yield return new WaitForSeconds(1);

                // フェードイン
                yield return panel.DOFade(1, 1).WaitForCompletion();

                // プレイヤーカメラに戻す + フェードアウト
                _playerCam.Priority = 1;
                star.StarCam.Priority = 0; // StarCamも忘れずに無効化
                yield return panel.DOFade(0, 1).WaitForCompletion();
                panel.gameObject.SetActive(false);
                star.IsCollected = true;
            }
        }
        CheckStars();
    }
}
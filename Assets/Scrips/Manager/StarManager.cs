using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager instance;//シングルトン

    [SerializeField] private List <Stars> _stars;//星共の配列
    [SerializeField] private GameObject brige;

    [System.Serializable]
    public class Stars
    {
        /// <summary>プレイヤーが入手するための星</summary>
        [SerializeField] private GameObject star;
        /// <summary>どの星をゲットしたか確認するためのオブジェクト</summary>
        [SerializeField] private GameObject collectStar;

        public GameObject Star { get => star; set => star = value; }
        public GameObject CollectStar { get => collectStar; set => collectStar = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (var star in _stars)
        {
            star.Star.SetActive(true);
            star.CollectStar.SetActive(false);
        }
        brige.SetActive(false);
    }
    /// <summary>
    /// 星をゲットしたらゴール前の星を表示する
    /// </summary>
    public void ActiveCollectStar()
    {
        foreach (var star in _stars)
        {
            if (star.Star.activeSelf == false)
            {
                star.CollectStar.SetActive(true);
            }
        }
        CheckStars();
    }
    /// <summary>
    /// 全てのCollectStarが有効になっているかチェックして橋を有効化する
    /// </summary>
    private void CheckStars()
    {
        foreach (var star in _stars)
        {
            if (star.CollectStar.activeSelf == false)
            {
                return;
            }
        }
        brige?.SetActive(true);
    }
}
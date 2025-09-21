using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMirrors : MonoBehaviour
{
    [SerializeField] private List<Transform> mirrors;
    [Header("ループの速さ")]
    [SerializeField] private float delay = 1f;
    private void Awake()
    {
        StartCoroutine(LoopMirror());
    }
    /// <summary>
    /// 鏡のポジションを降下順に入れ替えていく
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoopMirror()
    {
        while (true)//無限ループはするが一回ごとに待機
        {
            //最初の鏡
            var first = mirrors[0].position;
            for (int i = 0; i < mirrors.Count - 1; i++)
            {
                mirrors[i].position = mirrors[i + 1].position;
            }
            //最後に先頭に戻す
            mirrors[mirrors.Count - 1].position = first;
            yield return new WaitForSeconds(delay);
        }
    }
}

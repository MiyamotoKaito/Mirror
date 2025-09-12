using System.Collections;
using TMPro;
using UnityEngine;

public class TextWrite : MonoBehaviour
{
    [Header("読み込むテキスト")]
    [SerializeField] private TextMeshProUGUI loadText;

    [Header("読み込む速度")]
    [SerializeField] private float writeSpeed;

    private Coroutine _coroutine;

    /// <summary>
    /// 文字送りの演出
    /// </summary>
    public void Write()
    {
        //前回の処理が走っていたら停止
        if (_coroutine != null)
        {
            StopCoroutine(ShowText());
        }
        _coroutine = StartCoroutine(ShowText());
    }
    private IEnumerator ShowText()
    {
        //待機時間
        var delay = new WaitForSeconds(writeSpeed);

        //テキスト全体の長さ
        var textLength = loadText.text.Length;

        //一文字ずつ表示
        for (int i = 0; i < textLength; i++)
        {
            loadText.maxVisibleCharacters = Mathf.Min(i, textLength);
            yield return delay;
        }
        // 最終的に全文字を表示
        loadText.maxVisibleCharacters = textLength;
        _coroutine = null;
    }
}

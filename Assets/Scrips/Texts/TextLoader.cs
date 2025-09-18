using System.Collections;
using TMPro;
using UnityEngine;
using System.Linq;

public class TextLoader : MonoBehaviour
{
    [Header("読み込むためのテキスト")]
    [SerializeField] private TextMeshProUGUI logText;
    [Header("読み込む速度")]
    [SerializeField] private float writeSpeed;
    [Header("表示させるテキスト")]
    [SerializeField, TextArea(2, 5)]
    private string log;

    private GameObject _player;
    private AudioSource _audioSource;

    private Coroutine _coroutine;
    private void Awake()
    {
        logText.enabled = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = _player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Write();
        logText.enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Write();
        logText.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        logText.enabled = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        logText.enabled = false;
    }
    /// <summary>
    /// 文字送りの演出
    /// </summary>
    private void Write()
    {
        AudioManager.Instance.PlaySE("タイピング", _audioSource);
        logText.text = log;
        if (_coroutine != null)
        {
            //前回の処理が走っていたら停止
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        //次の文字を表示させるための待機時間
        var delay = new WaitForSeconds(writeSpeed);
        //テキスト全体の長さ
        var textLength = logText.text.Length;
        //一文字ずつ表示する
        for (int i = 0; i <= textLength; i++)
        {
            logText.maxVisibleCharacters = i;
            //一定時間待機
            yield return delay;
        }
        AudioManager.Instance.Stop(_audioSource);
        _coroutine = null;
    }
}
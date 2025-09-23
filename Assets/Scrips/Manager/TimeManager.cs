using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField, Header("タイマーのテキスト")]
    private TextMeshProUGUI timerText;
    [SerializeField, Header("スポットライト")]
    private List<Light> spotLights;
    [SerializeField, Header("タイムオーバー用のパネル")]
    private Image panel;
    [SerializeField, Header("タイトルに戻るボタン")]
    private Button button;
    [SerializeField, Header("時間制限")]
    private float timer;

    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private bool isAlert;
    private void Awake()
    {
        timerText.enabled = true;
        panel.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }
    private void Start()
    {
        _audioSource = AudioManager.Instance.gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F0");
        Alert();
        TimeOver();
    }
    /// <summary>
    /// 何秒か経過したらライトで警告を出す
    /// </summary>
    private void Alert()
    {
        if (timer < 30 && !isAlert)
        {
            _coroutine = StartCoroutine(ChangeTextColor());

            foreach (Light light in spotLights)
            {
                light.color = Color.red;
                AudioManager.Instance.PlaySE("アラート", _audioSource);
            }
            isAlert = true;
        }
    }
    /// <summary>
    /// タイマーが0になったらタイトルに戻す
    /// </summary>
    private void TimeOver()
    {
        if (timer < 0)
        {
            // コルーチンを停止
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            InputSystem.DisableDevice(Keyboard.current);
            InputSystem.DisableDevice(Gamepad.current);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            panel.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            timerText.enabled = false;
            panel.DOFade(1, 2);
        }
    }
    /// <summary>
    /// テキストを拡大、赤くするコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeTextColor()
    {
        while (true)
        {
            //毎回新しSequenceを作成
            Sequence sq = DOTween.Sequence();

            sq.Append(DOTween.To(() => timerText.fontSize, x => timerText.fontSize = x, 220, 0.5f))
             .Join(timerText.DOColor(Color.red, 0.5f))
             .Append(DOTween.To(() => timerText.fontSize, x => timerText.fontSize = x, 130, 0.5f))
             .Join(timerText.DOColor(Color.white, 0.5f));

            yield return new WaitForSeconds(0.5f);
        }
    }
}

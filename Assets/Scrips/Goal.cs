using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    /// <summary>ゴールしたら表示するパネル</summary>
    [SerializeField, Header("ゴール用のパネル")] private Image goalPanel;
    /// <summary>スキップしたときにフェードインするパネル</summary>
    [SerializeField, Header("フェードイン用のパネル")]
    private Image fadePanel;
    /// <summary>クレジットの流れるスピード</summary>
    [SerializeField, Header("クレジットの流れるスピード")]
    private float speed;
    /// <summary>スキップ用のボタン</summary>
    [SerializeField, Header("スキップボタン")]
    private Button button;
    /// <summary>クレジットで流すテキスト</summary>
    [SerializeField, Header("クレジットで流すテキスト")]
    private TextMeshProUGUI creditText;
    /// <summary>表示したいテキスト</summary>
    [SerializeField, Header("表示したいテキスト"), TextArea(1, 20)]
    private string credit;
    [SerializeField]
    private TextMeshProUGUI timerText;

    private AudioSource _audioSource;
    private bool isScrolling;
    private void Awake()
    {
        goalPanel.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        EnableInput();
    }
    private void Start()
    {
        creditText.text = credit;
        _audioSource = AudioManager.Instance.gameObject.GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("ああああ");
        }
    }
    private void Update()
    {
        //一番下まで来たら止める
        if (isScrolling)
        {
            Credit();
        }
    }
    /// <summary>
    /// プレイヤーを動かないようにしてエンディングを流す
    /// </summary>
    public void GameClear()
    {
        goalPanel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isScrolling = true;
        timerText.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
        AudioManager.Instance.PlayBGM("Ending", _audioSource);
    }
    /// <summary>
    /// クレジットを流す
    /// </summary>
    private void Credit()
    {
        DisableInput();
        var currentPos = goalPanel.rectTransform.position;
        currentPos.y += speed * Time.deltaTime;
        goalPanel.rectTransform.position = currentPos;
        if (currentPos.y > 1000)
        {
            isScrolling = false;
            SkipCredit();
        }
    }
    /// <summary>
    /// クレジットスキップ
    /// </summary>
    public void SkipCredit()
    {
        EnableInput();
        fadePanel.gameObject.SetActive(true);
        AudioManager.Instance.Stop(_audioSource);
        fadePanel.DOFade(1, 1)
            .OnComplete(() => { SceneManager.LoadScene("Title"); });
    }
    private void EnableInput()
    {
        //キーボードを有効化
        InputSystem.EnableDevice(Keyboard.current);
        //パッドを有効化
        InputSystem.EnableDevice(Gamepad.current);
    }
    /// <summary>
    /// 入力の無効化
    /// </summary>
    private void DisableInput()
    {
        //キーボードを無効化
        InputSystem.DisableDevice(Keyboard.current);
        //パッドを無効化
        InputSystem.DisableDevice(Gamepad.current);
    }
}

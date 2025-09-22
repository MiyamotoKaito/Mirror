using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static DeviceCheck;

public class DeviceCheck : MonoBehaviour
{
    /// <summary>デバイステキストのリスト</summary>
    [SerializeField] private List<DeviceText> deviceTexts;
    /// <summary>デバイス画像のリスト</summary>
    [SerializeField] private List<DeviceImage> deviceImages;
    /// <summary>現在のデバイス</summary>
    private InputDevice _currentdevice;
    /// <summary>前回のデバイス</summary>
    private InputDevice _lastDevice;

    /// <summary>
    /// デバイスによるテキストの情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class DeviceText
    {
        /// <summary>表示するテキスト</summary>
        [SerializeField] private TextMeshProUGUI displayText;
        /// <summary>キーボード用のテキスト</summary>
        [SerializeField] private string keyboardText;
        /// <summary>パッド用のテキスト</summary>
        [SerializeField] private string padText;

        public TextMeshProUGUI DisplayText => displayText; 
        public string KeyboardText => keyboardText; 
        public string PadText => padText;
    }
    /// <summary>
    /// デバイスによる画像の情報を管理するクラス
    /// </summary>
    [System.Serializable]
    public class DeviceImage
    {
        /// <summary>表示するUI画像</summary>
        [SerializeField] private Image displayImage;
        /// <summary>キーボード用の画像</summary>
        [SerializeField] private Sprite keyboradImage;
        /// <summary>パッド用の画像</summary>
        [SerializeField] private Sprite padImage;

        public Image DisplayImage => displayImage;
        public Sprite KeyboradImage => keyboradImage;
        public Sprite PadImage => padImage;
    }

    void Update()
    {
        CheckCurrentDevice();

        if (_currentdevice != _lastDevice)
        {
            _lastDevice = _currentdevice;
            UpdateDisplayUI();
        }
    }
    /// <summary>
    /// 今使っているデバイスを検知する
    /// </summary>
    private void CheckCurrentDevice()
    {
        //最近のキーボード入力をチェック
        if (Keyboard.current.wasUpdatedThisFrame)
        {
            _currentdevice = Keyboard.current;
            return;
        }
        //最近のパッド入力をチェック
        if (Gamepad.current.wasUpdatedThisFrame)
        {
            _currentdevice = Gamepad.current;
            return;
        }
        //何も入力が無かった場合キーボードをデフォルトに設定
        if (_currentdevice == null)
        {
            _currentdevice = Keyboard.current;
        }
    }
    /// <summary>
    /// 検知したデバイスを元にテキストの表示する
    /// </summary>
    private void UpdateDisplayUI()
    {
        if (deviceTexts == null) return;

        //テキストの表示
        foreach (var deviceText in deviceTexts)
        {
            //キーボードのテキストの表示
            if (_currentdevice is Keyboard)
            {
                deviceText.DisplayText.text = deviceText.KeyboardText;
            }
            //パッドのテキスト表示
            else if (_currentdevice is Gamepad)
            {
                deviceText.DisplayText.text = deviceText.PadText;
            }
        }
        //画像の表示
        foreach (var deviceImage in deviceImages)
        {
            //キーボードの画像の表示
            if (_currentdevice is Keyboard)
            {
                deviceImage.DisplayImage.sprite = deviceImage.KeyboradImage;
            }
            //パッドの画像表示
            else if (_currentdevice is Gamepad)
            {
                deviceImage.DisplayImage.sprite = deviceImage.PadImage;
            }
        }
    }
}

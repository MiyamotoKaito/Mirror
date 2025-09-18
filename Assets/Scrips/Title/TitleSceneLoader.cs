using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
/// <summary>
///　タイトルでのシーンロードクラス
/// </summary>
public class TitleSceneLoader : PlayerBase
{
    /// <summary>鏡についているカメラとシーンの名前を持ったクラスの配列</summary>
    [SerializeField] private List<CameraInfo> scenes;
    [SerializeField] private GameObject moveText;
    /// <summary>
    /// カメラが持つシーンの情報
    /// </summary>
    [System.Serializable]
    public class CameraInfo
    {
        /// <summary>モード選択用のカメラ</summary>
        [SerializeField, Header("カメラ")]
        private CinemachineCamera cam;
        /// <summary>シーンの名前</summary>
        [SerializeField, Header("表示名")]
        private string sceneName;
        /// <summary>シーンの説明</summary>
        [SerializeField, Header("説明"),TextArea(1, 4)]
        private string sceneInfo;
        /// <summary>シーンが設定されているかどうか</summary>
        [SerializeField, Header("表示名")]
        private bool hasScene;
        /// <summary>ロードするシーンの名前</summary>
        [SerializeField, Header("ロードするシーンの名前(hasSceneがtrueのときのみ)")]
        private string loadSceneName;

        public CinemachineCamera Cam => cam;
        public string SceneName => sceneName;
        public string SceneInfo => sceneInfo;
        public bool HasScene => hasScene;
        public string LoadSceneName => loadSceneName;

    }
    private void Awake()
    {
        base.BaseAwake();
    }
    private void OnEnable()
    {
        _playerBase.Player.Attack.started += OnInputEnterScene;
    }
    private void OnDisable()
    {
        _playerBase.Player.Attack.started -= OnInputEnterScene;
        base.BaseOnDisable();
    }
    public CameraInfo GetCamsInfo()
    {
        //優先度が1のカメラを探す
        foreach (var scene in scenes)
        {
            if (scene.Cam.Priority == 1)
            {
               return scene;
            }
        }
        return null;
    }
    /// <summary>
    /// 優先度が1のカメラについているシーンをロードするメソッド
    /// </summary>
    /// <param name="context"></param>
    private void OnInputEnterScene(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var camsInfo = GetCamsInfo();
            //シーンを持っていたらロードする
            if (camsInfo.HasScene)
            {
                _playerBase.Disable();
                SceneManager.LoadScene(camsInfo.LoadSceneName);
                moveText.SetActive(false);
}
            else
            {
                Debug.Log("シーンを持っていない");
            }
        }
    }
}
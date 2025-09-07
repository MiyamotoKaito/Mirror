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
    [SerializeField] private List<Scene> scenes;

    [System.Serializable]
    public class Scene
    {
        /// <summary>モード選択用のカメラ</summary>
        [SerializeField] private CinemachineCamera cams;
        /// <summary>ロードするシーンの名前</summary>
        [SerializeField] private string sceneNames;

        public CinemachineCamera Cams { get { return cams; } }
        public string SceneName { get { return sceneNames; } }
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
    /// <summary>
    /// 優先度が1のカメラについているシーンをロードするメソッド
    /// </summary>
    /// <param name="context"></param>
    private void OnInputEnterScene(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //優先度が1のカメラを探してついているシーンをロードする
            foreach (Scene scene in scenes)
            {
                _playerBase.Player.Disable();
                if (scene.Cams.Priority == 1)
                {
                    SceneManager.LoadScene(scene.SceneName);
                }
            }
        }
    }
}
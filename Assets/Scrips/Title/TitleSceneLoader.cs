using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleSceneLoader : PlayerBase
{
    [SerializeField] private List<Scene> scenes;
    [System.Serializable]
    public class Scene
    {
        [SerializeField] private CinemachineCamera cams;
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
    private void OnInputEnterScene(InputAction.CallbackContext context)
    {
        if (context.started)
        {
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
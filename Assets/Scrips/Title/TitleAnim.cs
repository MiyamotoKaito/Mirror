using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
public class TitleAnim : MonoBehaviour
{
    /// <summary>スポットライトとカメラの配列</summary>
    [SerializeField] private List<SelectObjects> objs;
    /// <summary>モードセレクト全体を映し出すカメラ</summary>
    [SerializeField] private CinemachineCamera selectView;
    /// <summary>モードセレクトを管理するオブジェクト</summary>
    [SerializeField] private GameObject modeSelect;
    /// <summary>タイトル管理のオブジェクト</summary>
    [SerializeField] private GameObject titleManager;

    [SerializeField] private GameObject moveText;
    /// <summary>Managerのaudiosoure</summary>
    private AudioSource _audioSource;

    public UnityEvent Action;

    [System.Serializable]
    public class SelectObjects
    {
        /// <summary>スポットライト</summary>
        [SerializeField] private GameObject spotLights;
        /// <summary>スポットライトと同じオブジェクトの子供のカメラ</summary>
        [SerializeField] private CinemachineCamera cameras;

        public GameObject SpotLights { get { return spotLights; } }
        public CinemachineCamera Camera { get { return cameras; } }
    }
    private void Awake()
    {
        selectView.gameObject.SetActive(false);
        moveText.SetActive(false);
        modeSelect.SetActive(false);
        _audioSource = AudioManager.Instance.gameObject.GetComponent<AudioSource>();
        //配列に入っている全てのカメラとスポットライトを無効化
        foreach (SelectObjects obj in objs)
        {
            obj.Camera.gameObject.SetActive(false);
            obj.SpotLights.SetActive(false);
        }
    }
    /// <summary>
    /// カメラの切り替え
    /// </summary>
    public void SetCamera()
    {
        //カメラの切り替えとアニメーションの開始
        selectView.gameObject.SetActive(true);
        if (selectView.gameObject.activeSelf)
        {
            StartCoroutine(LightUp());
        }
        titleManager.SetActive(false);
    }
    /// <summary>
    /// ライトアップのアニメーションコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator LightUp()
    {
        //2秒待って1秒ごとにスポットライトとカメラを有効にする
        yield return new WaitForSeconds(2);
        foreach (SelectObjects obj in objs)
        {
            obj.Camera.gameObject.SetActive(true);
            obj.SpotLights.SetActive(true);
            AudioManager.Instance.PlaySE("点灯", _audioSource);
            yield return new WaitForSeconds(1f);
        }
        modeSelect.SetActive(true);
        Action.Invoke();
        AudioManager.Instance.PlayBGM("モードセレクト", _audioSource);
        moveText?.SetActive(true);
    }
}
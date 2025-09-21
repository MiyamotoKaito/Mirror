using UnityEngine;
using UnityEngine.Events;

public class NearByMirror : MonoBehaviour
{
    /// <summary>鏡</summary>
    [SerializeField] private GameObject mirror;
    /// <summary>鏡についてるレンダラーテクスチャ</summary>
    [SerializeField] private RenderTexture mirrorTex;

    public UnityEvent Action;

    private RenderTexture sourceTex;
    private void Awake()
    {
        sourceTex = mirrorTex;
        mirror.SetActive(false);
        mirrorTex.Release();//メモリ解放
    }
    private void OnTriggerEnter(Collider other)
    {
        Action.Invoke();
        mirror.SetActive(true);
        Graphics.Blit(sourceTex, mirrorTex);//書き込み
    }
    private void OnTriggerExit(Collider other)
    {
        mirror.SetActive(false);
        mirrorTex.Release();
    }
}
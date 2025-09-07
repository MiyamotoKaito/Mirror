using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 鏡から離れたら鏡の描画を無効化する
/// </summary>
public class NearByMirror : MonoBehaviour
{
    /// <summary>鏡の部分</summary>
    [SerializeField] private GameObject mirror;
    /// <summary>鏡に写ってるテクスチャ</summary>
    [SerializeField] private RenderTexture mirrorTex;

    private RenderTexture sourceTex;
    private void Awake()
    {
        sourceTex = mirrorTex;
        mirror.SetActive(false);
        mirrorTex.Release();//メモリ解放
    }
    private void OnTriggerEnter(Collider other)
    {
        mirror.SetActive(true);
        Graphics.Blit(sourceTex, mirrorTex);//書き込み
    }
    private void OnTriggerExit(Collider other)
    {
        mirror.SetActive(false);
        mirrorTex.Release();
    }
}
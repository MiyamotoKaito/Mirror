using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UIElements;

public class NearByMirror : MonoBehaviour
{
    [SerializeField] private GameObject mirror;
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
using TMPro;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    [SerializeField, Header("名前のテキスト")]
    private TextMeshProUGUI stagNameText;
    [SerializeField, Header("説明のテキスト")]
    private TextMeshProUGUI stageInfoText;
    [SerializeField, Header("ステージのリスト")]
    private TitleSceneLoader sceneLoader;
    private void Update()
    {
        ShowText();
    }
    /// <summary>
    /// UI上にステージのテキストを表示させる
    /// </summary>
    public void ShowText()
    {
        //コンストラクタからシーンの情報を入手
        var camsInfo = sceneLoader.GetCamsInfo();

        //シーンの情報を表示
        if (camsInfo != null)
        {
            stagNameText.enabled = true;
            stageInfoText.enabled = true;
            stagNameText.text = camsInfo.SceneName;
            stageInfoText.text = camsInfo.SceneInfo;
        }
        else
        {
            HideText();
        }
    }
    /// <summary>
    /// 全てのテキストを非表示
    /// </summary>
    private void HideText()
    {
        stagNameText.enabled = false;
        stageInfoText.enabled = false;
    }
}

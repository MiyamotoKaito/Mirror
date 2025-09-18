using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// インプットシステムのアクションを管理するクラス
/// </summary>
public class PlayerBase : MonoBehaviour
{
    protected InputSystem_Actions _playerBase;
    /// <summary>
    /// 入力アクションを呼び出す
    /// </summary>
    protected void BaseAwake()
    {
        _playerBase = new InputSystem_Actions();
        _playerBase.Enable();
    }
    /// <summary>
    /// 入力アクションを無効化する
    /// </summary>
    protected void BaseOnDisable()
    {
        _playerBase?.Disable();
    }
}
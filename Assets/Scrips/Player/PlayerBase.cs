using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected PlayerBase _playerBase;

    protected void Awake()
    {
        _playerBase = new PlayerBase();
        _playerBase.enabled = true;
    }
}

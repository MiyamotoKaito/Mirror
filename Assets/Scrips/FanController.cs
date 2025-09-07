using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private Transform fan;
    [SerializeField] private float rotateZ;
    private AudioSource _fanSource;
    bool _isStop;
    void Start()
    {
        _fanSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlayBGM("ファンの音", _fanSource);
    }
    void Update()
    {
        fan.Rotate(0, 0, rotateZ * Time.deltaTime);
        if (_isStop)
        {
            AudioManager.Instance.SmallerBGM(_fanSource);
            fan.Rotate(0, 0, rotateZ - Time.deltaTime);
            if (rotateZ == 0)
            {
                AudioManager.Instance.Stop(_fanSource);
                rotateZ = 0;
            }
        }
    }
}
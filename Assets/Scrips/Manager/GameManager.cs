using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string bgmName;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = AudioManager.Instance.gameObject.GetComponent<AudioSource>();
        AudioManager.Instance.PlayBGM(bgmName, _audioSource);
    }
}
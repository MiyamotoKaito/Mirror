using UnityEngine;

public class Star : MonoBehaviour
{
    private AudioSource _audioSource;
    private StarManager _starManager;
    private void Awake()
    {
        var manager = GameObject.Find("StarManager");
        _starManager = manager.GetComponent<StarManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        AudioManager.Instance.PlayBGM("星", _audioSource);
    }
    public void GetStar()
    {
        this.gameObject.SetActive(false);
        _starManager.ActiveCollectStar();
    }
}
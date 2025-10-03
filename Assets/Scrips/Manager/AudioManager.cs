using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class SoundClip
    {
        public string Name;
        public AudioClip Clip;
    }

    [SerializeField, Header("SEリスト")]
    private List<SoundClip> seList;

    [SerializeField, Header("BGMリスト")]
    private List<SoundClip> bgmList;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlaySE(string name, AudioSource audiosource)
    {
        audiosource?.PlayOneShot(seList.FirstOrDefault(clip => clip.Name == name)?.Clip);
    }
    public void PlayBGM(string name, AudioSource audiosource)
    {
        var clip = bgmList.FirstOrDefault(c => c.Name == name);
        if (clip != null)
        {
            audiosource.clip = clip.Clip;
            audiosource.loop = true;
            audiosource.Play();
        }
    }
    public void BiggerBGM(AudioSource audioSource)
    {
        audioSource.volume += 0.01f * Time.deltaTime;
    }
    public void SmallerBGM(AudioSource audioSource)
    {
        audioSource.volume -= 0.01f * Time.deltaTime;
    }
    public void Stop(AudioSource audioSource)
    {
        audioSource?.Stop();
    }
    public void Pause(AudioSource audioSource)
    {
        audioSource?.Pause();
    }
    public void UnPause(AudioSource audioSource)
    {
        audioSource?.Stop();
    }
}

using System.Collections.Generic;
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

    [SerializeField,Header("SEリスト")]
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
        foreach (var clip in seList)
        {
            if (clip.Name == name)
            {
                audiosource?.PlayOneShot(clip.Clip);
                break;
            }
        }
    }
    public void PlayBGM(string name, AudioSource audiosource)
    {
        foreach (var clip in bgmList)
        {
            if (clip.Name == name)
            {
                audiosource.clip = clip.Clip;
                audiosource.loop = true;
                audiosource.Play();
                break;
            }
        }
    }
    public void BiggerBGM(AudioSource audioSource)
    {
        audioSource.volume += 0.01f * Time.deltaTime;
    }
    public void SmallerBGM(AudioSource audioSource)
    {
        audioSource.volume -= 0.01f * Time.deltaTime ;
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

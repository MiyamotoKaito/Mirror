using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
    [SerializeField] private List<GameObject> spotLights;
    [SerializeField] private Canvas canvas;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        foreach (var light in spotLights)
        {
            light.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {

    }

    private IEnumerator LightUP()
    {
        for (int i = 0; i < spotLights.Count; i++)
        {
            spotLights[i].SetActive(true);
            AudioManager.Instance.PlaySE("点灯", _audioSource);
           yield return new WaitForSeconds(0.5f);
        }
    }
}

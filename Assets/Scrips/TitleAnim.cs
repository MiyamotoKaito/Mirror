using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
public class TitleAnim : MonoBehaviour
{
    [SerializeField] private List<SelectObjects> objs;
    [SerializeField] private CinemachineCamera selectView;

    [SerializeField] private AudioSource audioSource;

    [System.Serializable]
    public class SelectObjects
    {
        [SerializeField] private GameObject spotLights;
        [SerializeField] private CinemachineCamera cameras;

        public GameObject SpotLights { get { return spotLights; } }
        public CinemachineCamera Camera { get { return cameras; } }
    }
    private void Awake()
    {
        foreach (SelectObjects obj in objs)
        {
            obj.Camera.gameObject.SetActive(false);
            obj.SpotLights.SetActive(false);
        }
    }
    public void Light()
    {
        if (selectView.gameObject.activeSelf)
        {
            StartCoroutine(LightUp());
        }
    }
    private IEnumerator LightUp()
    {
        yield return new WaitForSeconds(2);
        foreach (SelectObjects obj in objs)
        {
            obj.Camera.gameObject.SetActive(true);
            obj.SpotLights.SetActive(true);
            AudioManager.Instance.PlaySE("点灯", audioSource);
            yield return new WaitForSeconds(1f);
        }
    }
}

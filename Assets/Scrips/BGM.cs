using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance;

    [SerializeField] private Camera firstCam;
    [SerializeField] private Camera secondCam;

    private void Awake()
    {
        transform.position = firstCam.transform.position;
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
    private void Update()
    {
        //ChengeTarget();
        transform.position = transform.position;
    }

    //private void ChengeTarget()
    //{
    //    if (!firstCam.gameObject.activeSelf)
    //    {
    //        transform.position = secondCam.transform.position;
    //    }
    //}
}
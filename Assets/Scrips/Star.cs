using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnDisable()
    {
        StarManager.instance.ActiveCollectStar();
    }
    public void GetStar()
    {
        this.gameObject.SetActive(false);
    }
}
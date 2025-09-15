using UnityEngine;

public class Star : MonoBehaviour
{
    public void GetStar()
    {
        this.gameObject.SetActive(false);
        StarManager.instance.ActiveCollectStar();
    }
}
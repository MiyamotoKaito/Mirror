using UnityEngine;
using DG.Tweening;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;

    public void OpenTheDoor()
    {
        right.DOMoveX(-2, 1f);
        left.DOMoveX(-2, 1f);

        Tween r = right.DOMoveZ(-2, 1f);
        Tween l = left.DOMoveZ(2, 1f);

        r.SetDelay(1f);
        l.SetDelay(1f);

    }
}

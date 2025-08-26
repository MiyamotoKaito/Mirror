using Unity.VisualScripting;
using UnityEngine;

public class MirrorWarp : MonoBehaviour
{
    [SerializeField] private Transform anotherMirrorPos;
    private bool inMirror;

    private void OnTriggerEnter(Collider other)
    {
        if (inMirror == false)
        {
            Debug.Log("ì¸Ç¡ÇƒÇ´ÇΩ");
            var x = anotherMirrorPos.position.x;
            var y = anotherMirrorPos.position.y;
            var z = anotherMirrorPos.position.z + 2f;
            other.transform.position = new Vector3(x, y, z);
            inMirror = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inMirror = false;
        Debug.Log("î≤ÇØèoÇµÇΩ");
    }
}

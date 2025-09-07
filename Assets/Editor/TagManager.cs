#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


#if UNITY_EDITOR
[CustomEditor(typeof(AddParentTag))]
public class AddTag : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AddParentTag tagApplier = (AddParentTag)target;
        GameObject parentObject = tagApplier.gameObject;
        if (GUILayout.Button("タグを付ける"))
        {
            foreach (Transform child in parentObject.transform)
            {
                child.tag = parentObject.tag;
                foreach (Transform grandChild in child.transform)
                {
                    grandChild.tag = parentObject.tag;
                    foreach (Transform greatGroundChild in grandChild)
                    {
                        greatGroundChild.tag = parentObject.tag;
                    }
                }
            }
            Debug.Log($"AddTag : {parentObject.tag}");
        }
    }
}
#endif
using UnityEngine;
using System.Collections;

namespace Systems.Utility.Database
{
    public class SODatabase<T> : AbstractDatabase<T> where T : SODatabaseAsset
    {
        protected override void OnAddObject(T t)
        {
#if UNITY_EDITOR
            t.hideFlags = UnityEngine.HideFlags.HideInHierarchy;
            UnityEditor.AssetDatabase.AddObjectToAsset(t, this);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        protected override void OnRemoveObject(T t)
        {
#if UNITY_EDITOR
            DestroyImmediate(t, true);
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}

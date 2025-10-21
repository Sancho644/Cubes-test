using System.Runtime.CompilerServices;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearChildren(this Transform transform)
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                foreach (Transform child in transform)
                {
                    Object.DestroyImmediate(child.gameObject, false);
                }

                return;
            }
#endif

            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
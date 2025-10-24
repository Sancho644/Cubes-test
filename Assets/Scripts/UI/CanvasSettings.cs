using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "CanvasSettings",
        menuName = "ScriptableObjects/CanvasSettingsScriptableObject", order = 2)]
    public class CanvasSettings : ScriptableObject
    {
        public Canvas Canvas { get; private set; }
        
        public void SetCanvas(Canvas canvas)
        {
            Canvas = canvas;
        }
    }
}
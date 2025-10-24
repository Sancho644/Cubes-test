using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConfigScriptableObject", order = 1)]
    public class ConfigScriptableObject : ScriptableObject
    {
        [SerializeField] private ConfigData data;

        public ConfigData Data => data;
    }
}
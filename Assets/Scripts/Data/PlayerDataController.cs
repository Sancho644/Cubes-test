using UnityEngine;
using Zenject;

namespace Data
{
    public class PlayerDataController : MonoBehaviour
    {
        [Inject] private readonly PlayerDataContainer _playerDataContainer;

        private void Start()
        {
            var hasSaves = _playerDataContainer.HasSave;
            if (!hasSaves)
            {
                _playerDataContainer.MarkPlayerDataIsDirty();
            }
        }

        private void LateUpdate()
        {
            SavePlayerData();
        }

        private void OnDestroy()
        {
            SavePlayerData();
        }

        private void SavePlayerData()
        {
            if (_playerDataContainer.IsDirty)
            {
                _playerDataContainer.SavePlayerData();
            }
        }
    }
}
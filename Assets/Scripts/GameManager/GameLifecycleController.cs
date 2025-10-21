using Data;
using UnityEngine;
using Zenject;

namespace GameManager
{
    public class GameLifecycleController : MonoBehaviour
    {
        [Inject] private readonly PlayerDataContainer _playerDataContainer;
        
        private void OnApplicationQuit()
        {
            ApplicationClosed();
        }

        private void ApplicationClosed()
        {
            Debug.Log("Application closed");
            _playerDataContainer.SavePlayerData();
        }
    }
}
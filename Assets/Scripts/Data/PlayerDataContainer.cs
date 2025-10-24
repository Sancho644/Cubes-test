using System.IO;
using System.Runtime.Serialization;
using Localization.Config;
using UnityEngine;
using Zenject;

namespace Data
{
    public class PlayerDataContainer
    {
        private const string PlayerDataPrefsKey = "PlayerDataSave";

        [Inject] private readonly IInstantiator _instantiator;

        public PlayerData Data { get; private set; }
        public bool HasSave { get; private set; }
        public bool IsDirty { get; private set; }

        public void SavePlayerData()
        {
            try
            {
                var jsonData = JsonUtility.ToJson(Data, true);
                PlayerPrefs.SetString(PlayerDataPrefsKey, jsonData);
                PlayerPrefs.Save();
                IsDirty = false;
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError(e);
            }
        }

        [Inject]
        public void LoadPlayerData()
        {
            Data = _instantiator.Instantiate<PlayerData>();

            Debug.Log("Loading player data");
            try
            {
                if (PlayerPrefs.HasKey(PlayerDataPrefsKey))
                {
                    var jsonData = PlayerPrefs.GetString(PlayerDataPrefsKey);
                    JsonUtility.FromJsonOverwrite(jsonData, Data);
                    HasSave = true;
                }
                else
                {
                    Data = CreateDefaultPlayerData();
                    HasSave = false;
                }
            }
            catch (SerializationException e)
            {
                Debug.LogError(e);
            }
        }

        public void MarkPlayerDataIsDirty()
        {
            IsDirty = true;
        }

        private PlayerData CreateDefaultPlayerData()
        {
            var playerData = _instantiator.Instantiate<PlayerData>();
            playerData.languageData.localizationType = LocalizationType.English;

            return playerData;
        }
    }
}
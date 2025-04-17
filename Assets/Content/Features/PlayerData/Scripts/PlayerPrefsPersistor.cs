using UnityEngine;

namespace Content.Features.PlayerData.Scripts
{
    public class PlayerPrefsPersistor<T> : BasePersistor<T> where T : IDataModel
    {
        public sealed override string PlayerPrefsKey { get; protected set; }

        public PlayerPrefsPersistor(string playerPrefsKey)
        {
            PlayerPrefsKey = playerPrefsKey;
        }

        public override void SaveData()
        {
            var json = JsonUtility.ToJson(_dataModel);
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            PlayerPrefs.Save();
        }

        public override void LoadData()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                var json = PlayerPrefs.GetString(PlayerPrefsKey);
                _dataModel = JsonUtility.FromJson<T>(json);
            }
        }
    }
}
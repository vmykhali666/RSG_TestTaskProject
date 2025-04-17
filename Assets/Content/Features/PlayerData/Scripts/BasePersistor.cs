using System;
using Zenject;

namespace Content.Features.PlayerData.Scripts
{
    public abstract class BasePersistor<T> : IPlayerDataPersistor<T, T>, IInitializable, IDisposable
        where T : IDataModel
    {
        public abstract string PlayerPrefsKey { get; protected set; }

        protected T _dataModel;
        public event Action<T> OnDataModelUpdated;

        public virtual T GetDataModel() => _dataModel;

        public virtual void UpdateModel(T dataModel)
        {
            _dataModel = dataModel;
            OnDataModelUpdated?.Invoke(_dataModel);
        }

        public abstract void SaveData();
        public abstract void LoadData();

        public virtual void Initialize()
        {
            LoadData();
        }

        public virtual void Dispose()
        {
            SaveData();
        }
    }
}
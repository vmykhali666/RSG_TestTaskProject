using Zenject;

namespace Content.Features.PlayerData.Scripts
{
    public interface IPlayerDataPersistor<out T, in TK>
        where T : IDataModel where TK : IDataModel
    {
        T GetDataModel();

        void UpdateModel(TK dataModel);
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content.Features.PlayerData.Scripts.Datas
{
    [CreateAssetMenu(
        menuName = "Configurations/Entity/" + "Container",
        fileName = nameof(EntitiesConfiguration) + "_Default", order = 0)]
    public class EntitiesConfiguration : ScriptableObject
    {
        [SerializeField] private List<EntityData> _entityDatas;
        [SerializeField] private EntityData _defaultEntityData;

        public T GetDataForEntity<T>() where T : EntityData
        {
            var entityData = _entityDatas.OfType<T>().FirstOrDefault();
            if (entityData == null)
            {
                Debug.LogWarning($"Item configuration of type {typeof(T)} not found.");
            }

            return entityData ?? _defaultEntityData as T;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.Datas
{
    [CreateAssetMenu(
        menuName = "Configurations/Entity/" + "Container",
        fileName = nameof(EntitiesConfiguration) + "_Default", order = 0)]
    public class EntitiesConfiguration : ScriptableObject
    {
        [SerializeField] private List<EntityData> _entityDatas;
        [SerializeField] private EntityData _defaultEntityData;

        public EntityData GetDataForEntity<T>() where T : EntityData
        {
            var _entityData = _entityDatas.OfType<T>().FirstOrDefault();
            if (_entityData != null)
            {
                Debug.LogWarning($"Item configuration of type {typeof(T)} not found.");
            }

            return _entityData ?? _defaultEntityData;
        }
    }
}
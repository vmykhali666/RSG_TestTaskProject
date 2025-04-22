using System;
using Content.Features.EntityAnimatorModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;
using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.EntityContext
{
    [Serializable]
    public class BaseEntityContext : IEntityContext, IEntityAnimatorContext, IEntityDataContext
    {
        [field: SerializeField] public IEntity Entity { get; set; }
        [field: SerializeField] public EntityAnimator EntityAnimator { get; set; }
        public EntityData EntityData { get; set; }
    }
}
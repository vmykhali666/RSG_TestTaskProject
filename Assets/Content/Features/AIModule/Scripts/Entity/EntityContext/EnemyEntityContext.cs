using System;
using Content.Features.DamageablesModule.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Content.Features.AIModule.Scripts.Entity.EntityContext
{
    [Serializable]
    public class EnemyEntityContext : BaseEntityContext, IDamageableContext, ITargetsFinderContext, INavigationContext
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; set; }
        [field: SerializeField] public TargetsFinder TargetsFinder { get; set; }
        [field: SerializeField] public IDamageable EntityDamageable { get; set; }
    }
}